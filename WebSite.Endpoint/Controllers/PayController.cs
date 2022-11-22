using System;
using System.Threading.Tasks;
using Application.Payments;
using Dto.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using WebSite.EndPoint.Utilities;
using ZarinPal.Class;

namespace WebSite.Endpoint.Controllers
{
    public class PayController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;
        private readonly string merchentId;

        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;


        public PayController(IConfiguration configuration, IPaymentService paymentService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            merchentId = configuration["ZarinpalMerchendId"];

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        // GET
        public async  Task<IActionResult> Index(Guid paymentId)
        {
            var payment = _paymentService.getPayment(paymentId);
            if (payment == null)
            {
                return NotFound();
            }

            string UserId = ClaimUtility.GetUserId(User);
            if (payment.Userid != UserId)
            {
                return BadRequest();
            }

            string CallbackUrl = Url.Action("Verify", "Pay", new {Id = payment.Id}, protocol: Request.Scheme);

            var resultZarinpalRequest =await _payment.Request(new DtoRequest()
            {
                Amount = payment.Amount,
                Description = payment.Description,
                CallbackUrl = CallbackUrl,
                Email = payment.Email,
                MerchantId = merchentId,
                Mobile = payment.PhoneNumber
            }, Payment.Mode.sandbox);

            return Redirect($"https://zarinpal.com/pg/StartPay/{resultZarinpalRequest.Authority}");
        }

        public IActionResult Verify(Guid Id , string Authority)
        {
            var status = HttpContext.Request.Headers["Status"];
            if (status != "" && status.ToString().ToLower() == "ok" 
                             && Authority != "")
            {
                var payment = _paymentService.getPayment(Id);
                if (payment == null)
                {
                    return NotFound();
                }
                //var verification = _payment.Verification(new DtoVerification
                //{
                //    Amount = payment.Amount,
                //    Authority = Authority,
                //    MerchantId = merchendId,
                //}, Payment.Mode.zarinpal).Result;

                var client = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", $"{{\"MerchantID\" :\"{merchentId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{payment.Amount}\"}}", ParameterType.RequestBody);

                var response = client.Execute(request);
                var verification = JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);
                if (verification.Status == 100)
                {
                   var res=   _paymentService.verifyPayment(Id, Authority, verification.RefID);
                   if (res)
                   {
                       return Redirect("/customers/orders");
                   }
                   else
                   {
                       TempData["message"] = "پرداخت انجام شد اما ثبت نشد";
                       return RedirectToAction("checkout", "basket");
                   }
                }
                else
                {
                    TempData["message"] = "پرداخت شما ناموفق بوده است . لطفا مجددا تلاش نمایید یا در صورت بروز مشکل با مدیریت سایت تماس بگیرید .";
                    return RedirectToAction("checkout", "basket");
                }
            }
            TempData["message"] = "پرداخت شما ناموفق بوده است .";
            return RedirectToAction("checkout", "basket");
        }
    }
    public class VerificationPayResultDto
    {
        public int Status { get; set; }
        public long RefID { get; set; }
    }
}
using System;
using System.Linq;
using Application.BasketService;
using Application.Discounts;
using Application.Orders;
using Application.Payments;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.Endpoint.Models.ViewModels.Baskets;
using WebSite.EndPoint.Utilities;
using static Domain.Orders.Address;

namespace WebSite.Endpoint.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IDiscountService _discountService;
        private readonly UserManager<User> _userManager;
        private string _userId = null;
        public BasketController(IBasketService basket, SignInManager<User> signInManager, IUserAddressService addressService, IOrderService orderService, IPaymentService paymentService, IDiscountService discountService, UserManager<User> userManager)
        {
            _basketService = basket;
            _signInManager = signInManager;
            _addressService = addressService;
            _orderService = orderService;
            _paymentService = paymentService;
            _discountService = discountService;
            _userManager = userManager;
        }
        // GET
        [AllowAnonymous]
        public IActionResult Index()
        {
            var data = GetOrCreateBasket();
            return View(data);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(int CatalogItemId, int quantity = 1)
        {
            var basket =  GetOrCreateBasket();
            _basketService.AddBasketItem(basket.Id , CatalogItemId , quantity);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RemoveItem(int ItemId)
        {
            _basketService.RemoveItemFromBasket(ItemId);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ChangeItemQuantity(int ItemId , int quantity)
        {
            return Json(_basketService.SetQuantities(ItemId, quantity));
            //return RedirectToAction("Index");
        }
        [AllowAnonymous]
        private BasketDto GetOrCreateBasket()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var UserId = ClaimUtility.GetUserId(User);
                return _basketService.GetOrCreateBasket(UserId);
            }
            else
            {
                SetCookieForUser();
               return _basketService.GetOrCreateBasket(_userId);
            }
        }

        public IActionResult ShippingPayment()
        {
            ShippingPaymentViewModel model = new ShippingPaymentViewModel();
            string userId = ClaimUtility.GetUserId(User);
            model.basket = _basketService.GetBasket(userId);
            model.UserAddress = _addressService.GetAddresses(userId);
            
            return View(model);
        }
        [HttpPost]
        public IActionResult ShippingPayment(int Address, PaymentMethod paymentMethod)
        {
            string userId = ClaimUtility.GetUserId(User);
            var basket = _basketService.GetBasket(userId);

            var OrderId = _orderService.CreateOrder(basket.Id, Address, paymentMethod);
            if (paymentMethod == PaymentMethod.OnlinePaymnt)
            {
                // ثبت پرداخت 
                var payment = _paymentService.PaymentOfOrder(OrderId);
                //ارسال به درگاه پرداخت 
                return RedirectToAction("Index", "Pay", new {paymentId = payment.PaymentId});
            }
            else
            {

                return RedirectToAction("Index", "Orders", new {area = "Customers"});
            }
            return View();
        }

        public IActionResult checkout()
        {
            // ایجاد ویو برای حالت های موفق ، ناموفق و ثبت نشدن سفارش برای ویو
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ApplyDiscount(int BasketId , string CouponCode)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var DiscountValid = _discountService.ValidateDiscount(CouponCode, user);
            
            if (DiscountValid.IsSucces == true)
            {
             _discountService.ApplyDiscountInBasket(CouponCode, BasketId);
            }
            else
            {
                TempData["InvalidMessage"] = String.Join(Environment.NewLine, DiscountValid.Message.Select(a => String.Join(", ", a)));
            }

            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult RemoveDiscount(int id)
        {
            _discountService.RemoveDiscountFromBasket(id);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        private void SetCookieForUser()
        {
            string BasketCookieName = "BasketId";
            if (Request.Cookies.ContainsKey(BasketCookieName))
            {
                _userId = Request.Cookies[BasketCookieName];
            }
            if (_userId != null) return;

            _userId = Guid.NewGuid().ToString();
            var cookieOption = new CookieOptions() { IsEssential = true, Expires = DateTime.Now.AddYears(2) };
            Response.Cookies.Append(BasketCookieName, _userId, cookieOption);

        }
    }
}
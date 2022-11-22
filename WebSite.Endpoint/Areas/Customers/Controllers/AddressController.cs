using Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Utilities;

namespace WebSite.Endpoint.Areas.Customers.Controllers
{
    [Authorize]
    [Area("Customers")]
    public class AddressController : Controller
    {
        private readonly IUserAddressService _addressService;

        // GET
        public AddressController(IUserAddressService addressService)
        {
            _addressService = addressService;
        }
        public IActionResult Index()
        {
            var data = _addressService.GetAddresses(ClaimUtility.GetUserId(User));

            return View(data);
        }
        public IActionResult AddNewAddress()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewAddress(AddUserAddressDto addressDto)
        {
            var UserId = ClaimUtility.GetUserId(User);
            addressDto.UserId = UserId;
            _addressService.AddNewAddress(addressDto);
            return RedirectToAction("Index");
        }
    }
}
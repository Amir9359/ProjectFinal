using Application.Orders.CustomerOrderServices;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Endpoint.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ICustomerOrderServices _customerOrderServices;
        private readonly UserManager<User> _userManager;
        public OrdersController(ICustomerOrderServices customerOrderServices, UserManager<User> userManager)
        {
            _customerOrderServices = customerOrderServices;
            _userManager = userManager;
        }

        // GET
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
             var data =  _customerOrderServices.getMyOrder(user.Id);
            return View(data);
        }
    }
}
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Endpoint.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize]
    public class MyFavouritController : Controller
    {
        private readonly ICatalogItemServices _catalogItemServices;
        private readonly UserManager<User>  _userManager;

        public MyFavouritController(ICatalogItemServices catalogItemServices, UserManager<User> userManager)
        {
            _catalogItemServices = catalogItemServices;
            _userManager = userManager;
        }

        // GET
        public IActionResult Index(int page = 1 , int pageSize = 20)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var data=  _catalogItemServices.GetMayFavourite(user.Id, page, pageSize);
            return View(data);
        }
        public IActionResult AddToMyFavourite(int catalogItemId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            _catalogItemServices.AddToFavorite(user.Id, catalogItemId);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFavorite(int catalogItemId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            _catalogItemServices.RemoveFavorite(user.Id, catalogItemId);
            return RedirectToAction("Index");
        }
    }
}
using System.Security.Claims;
using Application.BasketService;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Utilities;

namespace WebSite.Endpoint.Models.ViewComponents
{
    public class BasketComponent : ViewComponent
    {
        private readonly IBasketService _basketService;
        private ClaimsPrincipal userPrincipal => ViewContext?.HttpContext?.User;

        public BasketComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IViewComponentResult Invoke()
        {
            BasketDto basket = null;
            if (User.Identity.IsAuthenticated)
            {
                basket = _basketService.GetBasket(ClaimUtility.GetUserId(userPrincipal));
            }
            else
            {
                string BasketCookieName = "BasketId";
                if (Request.Cookies.ContainsKey(BasketCookieName))
                {
                    var buyerId = Request.Cookies[BasketCookieName];
                    basket = _basketService.GetBasket(buyerId);
                }
            }

            return View(viewName: "BasketComponent", model: basket);
        }
    }
}
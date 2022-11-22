using Application.Catalogs.GetMenuItems;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Endpoint.Models.ViewComponents
{
    public class GetMenuCategories : ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;

        public GetMenuCategories(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _getMenuItemService.Excute();
            return View(viewName: "GetMenuCategories", model: data);
        }
    }
}
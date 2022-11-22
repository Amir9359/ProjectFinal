using Application.Catalogs.CatalogItems.CatalogItemServices;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Endpoint.Models.ViewComponents
{
    public class BrandFilter : ViewComponent
    {
        private readonly ICatalogItemServices _catalogItemServices;

        public BrandFilter(ICatalogItemServices catalogItemServices)
        {
            _catalogItemServices = catalogItemServices;
        }

        public IViewComponentResult Invoke()
        {
            var brand = _catalogItemServices.GetBrand();
            return View("BrandFilter", brand);
        }
    }
}
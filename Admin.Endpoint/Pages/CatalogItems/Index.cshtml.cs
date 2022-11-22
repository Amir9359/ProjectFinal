using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogItems
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICatalogItemServices _catalogItem;

        public IndexModel(ICatalogItemServices catalogItem)
        {
            _catalogItem = catalogItem;
        }
        public PaginatedItemDto<CatalogItemListItemDto> CatalogItemsDto { get; set; }
        public void OnGet(int page =1 , int pageSize = 100)
        {
            CatalogItemsDto = _catalogItem.GetCatalogList(page, pageSize);
        }
    }
}

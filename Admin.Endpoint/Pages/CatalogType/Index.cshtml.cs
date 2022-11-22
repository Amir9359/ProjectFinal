using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Catalogs.CatalogTypes;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        public PaginatedItemDto<CatalogTypeListDto> catalogType { get; set; }

        public IndexModel(ICatalogTypeService catalogTypeService)
        {
            _catalogTypeService = catalogTypeService;
        }

        public void OnGet(int? parentid, int pageIndex = 1, int pageSize = 5)
        {
            catalogType = _catalogTypeService.GetList(parentid, pageIndex, pageSize);
        }
    }
}

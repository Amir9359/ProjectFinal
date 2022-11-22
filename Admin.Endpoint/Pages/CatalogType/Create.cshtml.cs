using System.Collections.Generic;
using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly IMapper _mapper;

        public CreateModel(ICatalogTypeService catalogTypeService, IMapper mapper)
        {
            _catalogTypeService = catalogTypeService;
            _mapper = mapper;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel { };
        public List<string> Message{ get; set; } = new List<string>();
        public void OnGet(int? parentId)
        {
            CatalogType.ParentCatalogTypeId = parentId;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Model =  _mapper.Map<CatalogTypeDto>(CatalogType);  
            var res = _catalogTypeService.Add(Model);
            if (res.IsSucces)
            {
                return RedirectToPage("Index", new {parentid = CatalogType.ParentCatalogTypeId});
            }

            Message = res.Message;
            return Page();
        }
    }
}

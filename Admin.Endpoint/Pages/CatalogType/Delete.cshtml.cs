using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogType
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly IMapper _mapper;

        public DeleteModel(ICatalogTypeService catalogTypeService, IMapper mapper)
        {
            _catalogTypeService = catalogTypeService;
            _mapper = mapper;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel { };
        public List<string> Message { get; set; } = new List<string>();
        public void OnGet(int id)
        {
            var model = _catalogTypeService.FindById(id);
            if (model.IsSucces)
                CatalogType =   _mapper.Map<CatalogTypeViewModel>(model.Data);
            Message = model.Message;
        }

        public IActionResult OnPost()
        {
           var res =  _catalogTypeService.Remove(CatalogType.Id);
           Message = res.Message;
           if (res.IsSucces)
           {
               return RedirectToPage("Index");
           }

           return Page();
        }
    }
}

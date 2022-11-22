using Application.Catalogs.CatalogItems.AddnewCatalogItem;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Dtos;
using AutoMapper;
using Domain.Catalogs;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Endpoint.Pages.CatalogItems
{
    [Authorize]
    public class EditeModel : PageModel
    {
        private readonly IAddnewCatalogItem _addnewCatalogItem;
        private readonly ICatalogItemServices _catalogItemServices;
        private readonly IImageUploadService _imageUpload;
        private readonly IMapper _mapper;


        public EditeModel(IAddnewCatalogItem addNewCatalogItemService
            , ICatalogItemServices catalogItemService, IImageUploadService imageUpload, IMapper mapper)
        {
            _addnewCatalogItem = addNewCatalogItemService;
            _catalogItemServices = catalogItemService;
            _imageUpload = imageUpload;
            _mapper = mapper;
        }
        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }


        public CatalogItemItemDto CatalogItems { get; set; }

        [BindProperty]
        public AddNewCatalogItemDto Data { get; set; }
        [BindProperty]
        public List<IFormFile> Files { get; set; }
        [BindProperty] 
        public int Id { get; set; }
        public void OnGet(int id)
        {

            CatalogItems = _catalogItemServices.GetCatalogItem(id);
 
            Categories = new SelectList(_catalogItemServices.GetCatalogType(), "Id", "Type", CatalogItems.CatalogType);
            Brands = new SelectList(_catalogItemServices.GetBrand(), "Id", "Brand", CatalogItems.Brand);
            
        }
        public JsonResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new JsonResult(new BaseDto<List<string>>(allErrors.Select(p => p.ErrorMessage).ToList(), new List<string> { "0" }, false));
            }

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                Files.Add(file);
            }

            var slug = Data.Slug.Replace(" ", "-");
            var images = new List<AddNewCatalogItemImage_Dto>();
            if (Files.Count > 0)
            {
                var result = _imageUpload.UploadImages(Files);
                foreach (var item in result)
                {
                    images.Add(new AddNewCatalogItemImage_Dto() { Src = item });
                }
            }
            Data.Images = images;
            Data.Slug = slug;
            var res = _addnewCatalogItem.Edite(Data ,Id);
            return new JsonResult(res);
        }
    }
}

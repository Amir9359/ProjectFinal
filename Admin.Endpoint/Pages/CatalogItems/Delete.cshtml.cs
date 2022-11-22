using Application.Catalogs.CatalogItems.CatalogItemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.CatalogItems
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ICatalogItemServices _catalogItemServices;

        public DeleteModel(ICatalogItemServices catalogItemServices)
        {
            _catalogItemServices = catalogItemServices;
        }

        [BindProperty]
        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            return;
        }
        public IActionResult OnPost(int id)
        {
            _catalogItemServices.RemoveCatalogItem(id);
            return RedirectToPage("Index");
        }

    }
}

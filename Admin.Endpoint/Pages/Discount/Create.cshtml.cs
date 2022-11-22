using Admin.Endpoint.Binders;
using Application.Discounts.AddNewDiscount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.Discount
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IAddNewDiscountService _discountService;

        public CreateModel(IAddNewDiscountService discountService)
        {
            _discountService = discountService;
        }
        [ModelBinder(BinderType = typeof(DiscountEntityBinder))]
        [BindProperty]
        public AddNewDiscountDto model { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            _discountService.excute(model);
        }
    }
}

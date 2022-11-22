using System.Threading.Tasks;
using Application.Discounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace Admin.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountApiController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountApiController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        [Route("SearchCatalogItems")]
        public async Task<IActionResult> SearchCatalogItems(string term)
        {
            return Ok(_discountService.getCatalogItems(term));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Application.Banners;
using Infrastructure.CashHelpers;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace Admin.Endpoint.Pages.Banner
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IImageUploadService _imageUploadService;
        private readonly IBannersService _bannersService;
        private readonly IDistributedCache _distributedCache;

        public CreateModel(IBannersService bannersService, IImageUploadService imageUploadService,
            IDistributedCache distributedCache)
        {
            _bannersService = bannersService;
            _imageUploadService = imageUploadService;
            _distributedCache = distributedCache;
        }

        [BindProperty]
        public BannerDto Banner { get; set; }

        [BindProperty]
        public IFormFile BannerImage { get; set; }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            var res = _imageUploadService.UploadImages(new List<IFormFile>() {BannerImage});
            if (res.Count > 0)
            {
                Banner.Image = res.FirstOrDefault();
                _bannersService.AddBanner(Banner);
                _distributedCache.Remove(CashHelper.GetHomePageChashKey());
            }

            return RedirectToPage("Index");
        }
    }
}

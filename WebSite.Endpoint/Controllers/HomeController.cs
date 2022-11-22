using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.HomepageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using WebSite.Endpoint.Models;
using WebSite.Endpoint.Utilities.Filters;
using Infrastructure.CashHelpers;

namespace WebSite.Endpoint.Controllers
{
    [ServiceFilter(typeof(SaveVisitorFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomepageService _homepageService;
        private readonly IDistributedCache _distributedCache;

        public HomeController(ILogger<HomeController> logger, IHomepageService homepageService, IDistributedCache distributedCache)
        {
            _logger = logger;
            _homepageService = homepageService;
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            var cash = _distributedCache.GetAsync(CashHelper.GetHomePageChashKey()).Result;
            HomePageDto HomePageData = new HomePageDto();
            if (cash != null)
            {
                 HomePageData = JsonSerializer.Deserialize<HomePageDto>(cash);
            }
            else
            {
                HomePageData = _homepageService.GetData();
                string JsonData = JsonSerializer.Serialize(HomePageData);
                byte[] encodedJson = Encoding.UTF8.GetBytes(JsonData);
                var cashOpt = new DistributedCacheEntryOptions().SetSlidingExpiration(CashHelper.DefaultDuration);
                _distributedCache.SetAsync(CashHelper.GetHomePageChashKey(), encodedJson, cashOpt);

            }
            return View(HomePageData);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

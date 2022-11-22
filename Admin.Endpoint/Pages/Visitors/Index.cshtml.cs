using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Visitors.GetTodyReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.Visitors
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IGetTodyReportService _getTodysService;
      
        public  ResultTodyReportDto ResultTodayReport;

        public IndexModel(IGetTodyReportService getTodysService)
        {
            _getTodysService = getTodysService;
        }

        public void OnGet()
        {
            ResultTodayReport = _getTodysService.Execute();
        }
    }
}

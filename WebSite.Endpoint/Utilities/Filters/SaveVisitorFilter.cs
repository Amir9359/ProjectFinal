using System;
using System.Linq;
using Application.Visitors.SaveVisitorInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace WebSite.Endpoint.Utilities.Filters
{
    public class SaveVisitorFilter : IActionFilter
    {
        private readonly ISaveVisitorInfoService _infoService;

        public SaveVisitorFilter(ISaveVisitorInfoService infoService)
        {
            _infoService = infoService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            string ControllerName =
                ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;

            string userAgent = context.HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            var UserInfo = uaParser.Parse(userAgent);

            string Referer = context.HttpContext.Request.Headers["Referer"].ToString();
            string CurrentUrl = context.HttpContext.Request.Path;
            var Request = context.HttpContext.Request;

            var visitorId = Request.Cookies["VisitorId"];

            _infoService.Execute(new VisitorDto()
            {
                Browser = new VisitorVersionDto()
                {
                    Family = UserInfo.UA.Family,
                    Version = $"{UserInfo.UA.Major}.{UserInfo.UA.Minor}.{UserInfo.UA.Patch}"
                },
                CurrentLink = CurrentUrl,
                ReferenceLink = Referer,
                Ip = ip,
                Method = Request.Method,

                Device = new DeviceDto()
                {
                    Family = UserInfo.Device.Family,
                    Model = UserInfo.Device.Model,
                    Brand = UserInfo.Device.Brand,
                    IsSpider = UserInfo.Device.IsSpider
                },
                OperationSystem = new VisitorVersionDto()
                {
                    Family = UserInfo.OS.Family,
                    Version = $"{UserInfo.OS.Major}.{UserInfo.OS.Minor}.{UserInfo.OS.Patch}.{UserInfo.OS.PatchMinor}"
                },
                PhysicalPath = $"{ControllerName}/{actionName}",
                Protocol = Request.Protocol,
                Time = DateTime.Now,
                VisitorId = visitorId
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
             
        }
    }
}
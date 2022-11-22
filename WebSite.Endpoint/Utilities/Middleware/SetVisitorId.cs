using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Endpoint.Utilities.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SetVisitorId
    {
        private readonly RequestDelegate _next;

        public SetVisitorId(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var visitorId = httpContext.Request.Cookies["VisitorId"];

            if (visitorId == null)
            {
                visitorId = Guid.NewGuid().ToString();
                httpContext.Request.HttpContext.Response.Cookies.Append("VisitorId", visitorId,
                    new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        Path = "/",
                        HttpOnly = true,
                        Expires = DateTime.Now.AddDays(30)

                    });
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SetVisitorIdExtensions
    {
        public static IApplicationBuilder UseSetVisitorId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetVisitorId>();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BasketService;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Catalogs.GetMenuItems;
using Application.Interfaces.Contexts;
using Application.Visitors.OnlineVisitors;
using Application.Visitors.SaveVisitorInfo;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Infrastructure.IdentityConfigs;
using Infrastructure.MappingProfile;
using Persistence.Context.MongoContext;
using WebSite.Endpoint.Hubs;
using WebSite.Endpoint.Utilities.Filters;
using WebSite.Endpoint.Utilities.Middleware;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Users;
using Application.Orders;
using Application.Payments;
using Application.Discounts;
using Application.Orders.CustomerOrderServices;
using Application.HomepageService;
using WebSite.Endpoint.Middleware;
using Application.Comments.Comment;
using MediatR;

namespace WebSite.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddMediatR(typeof(SendCommentCommand).Assembly);

            #region ConnectionString
            services.AddTransient<IDatabaseContext, DataBaseContext>();
            services.AddTransient<IIdentityDbContext, IdentityDatabaseContext>();

            string connectionString = Configuration["ConnectionStrings:sqlServer"];
            services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddDistributedSqlServerCache(option =>
            {
                option.ConnectionString = connectionString;
                option.SchemaName = "dbo";
                option.TableName = "tblCache";

            });

            services.AddIdentityService(Configuration);
            services.AddAuthorization();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/account/login";
                opt.AccessDeniedPath = "/account/AccessDenied";
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                opt.SlidingExpiration = true;
            });
            #endregion


            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
            services.AddTransient<ISaveVisitorInfoService, SaveVisitorInfoService>();
            services.AddTransient<IOnlineVisitorsService, OnlineVisitorsService>();
            services.AddTransient<IGetMenuItemService, GetMenuItemService>();
            services.AddTransient<IGetCatalogItemPLPService, GetCatalogItemPLPService>();
            services.AddTransient<IUriComposerService, UriComposerService>();
            services.AddTransient<IGetCatalogItemPDPService, GetCatalogItemPDPService>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IUserAddressService, UserAddressService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IDiscountUsageHistoryService, DiscountUsageHistoryService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<ICatalogItemServices, CatalogItemServices>();
            services.AddTransient<ICustomerOrderServices, CustomerOrderServices>();
            services.AddTransient<IHomepageService, HomepageService>();


            services.AddScoped<SaveVisitorFilter>();
            services.AddSignalR();

            //Mapper
            services.AddAutoMapper(typeof(CatalogMappingProfile));
            services.AddAutoMapper(typeof(UserMappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSetVisitorId();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "product",
                    pattern: "Product/{slug}",
                    defaults: new {controller = "Product", action = "Details"});

                endpoints.MapHub<OnlineVisitorHub>("/chatHub");
            });
        }
    }
}

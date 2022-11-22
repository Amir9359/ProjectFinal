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
using Application.Catalogs.CatalogTypes;
using Application.Interfaces.Contexts;
using Application.Visitors.GetTodyReport;
using Infrastructure.IdentityConfigs;
using Infrastructure.MappingProfile;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Context;
using Persistence.Context.MongoContext;
using Application.Catalogs.CatalogItems.AddnewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using FluentValidation;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Infrastructure.ExternalApi.ImageServer;
using Application.Discounts.AddNewDiscount;
using Application.Discounts;
using Application.Banners;
using Application.Catalogs.CatalogItems.UriComposer;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Admin.Endpoint
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
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllers();
            #region ConnectionString
            services.AddTransient<IDatabaseContext, DataBaseContext>();

            string connectionString = Configuration["ConnectionStrings:sqlServer"];
            services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddDistributedSqlServerCache(option =>
            {
                option.ConnectionString = connectionString;
                option.SchemaName = "dbo";
                option.TableName = "tblCache";

            });
            #endregion

            services.AddScoped<IGetTodyReportService, GetTodyReportService>();
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));

            services.AddIdentityService(Configuration);
            services.AddAuthorization();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);
                opt.SlidingExpiration = true;
                opt.LoginPath = "/account/login";
                opt.AccessDeniedPath = "/account/AccessDenied";
            });

            //Mapper
            services.AddAutoMapper(typeof(CatalogMappingProfile));
            services.AddAutoMapper(typeof(MappingProfiles.CatalogMappingProfile));
            services.AddTransient<ICatalogTypeService, CatalogTypeService>();
            services.AddTransient<IAddnewCatalogItem, AddnewCatalogItem>();
            services.AddTransient<ICatalogItemServices, CatalogItemServices>();
            services.AddTransient<IImageUploadService, ImageUploadService>();
            services.AddTransient<IAddNewDiscountService, AddNewDiscountService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IDiscountUsageHistoryService, DiscountUsageHistoryService>();
            services.AddTransient<IBannersService, BannersService>();
            services.AddTransient<IUriComposerService, UriComposerService>();

            // validator
            services.AddTransient<IValidator<AddNewCatalogItemDto>, AddNewCatalogItemDtoValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                
            });

        }
    }
}

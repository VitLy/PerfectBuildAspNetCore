﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerfectBuild.Data;
using PerfectBuild.Infrastructure;
using PerfectBuild.Models;
using PerfectBuild.Models.Interfaces;
using PerfectBuild.Models.Report;
using PerfectBuild.Services;
using PerfectBuild.Services.Report;
using System.Globalization;

namespace PerfectBuild
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("AzureConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<ITrainigDayConverter, TrainingDayConverter>();
            services.AddTransient<DocumentSpecHandler<TrainingPlanSpec>>();
            services.AddTransient<DocumentHeadHandler<TrainingHead>>();
            services.AddTransient<DocumentSpecHandler<TrainingSpec>>();
            services.AddTransient<ChartProvider, CanvasJSProvider>();
            services.AddSingleton<SpecLineValidator>();


            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new FloatModelBinderProvider());
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

            services.AddAuthentication().AddFacebook(option =>
            {
                option.AppId = Configuration["Authentication:Facebook:AppId"];
                option.AppSecret = Configuration["Authentication:Facebook:AppSecret"];

            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };
                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseMvc(routers =>
            {
                routers.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            IdentitySeedData.EnsurePopulation(app, Configuration);
        }
    }
}

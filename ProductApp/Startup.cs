using AspNetCore.Identity.Mongo;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerSideAnalytics;
using ServerSideAnalytics.Mongo;
using ProductApp.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;


namespace ProductApp
{
    public class Startup
    {
        private string ConnectionString => Configuration.GetConnectionString("DefaultConnection");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<MongoDBSettings>(
             options =>
             {
                 options.ConnectionString = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
                 options.DatabaseName = Configuration.GetSection("ConnectionStrings:DatabaseName").Value;
             });

           

            var store = GetAnalyticStore(ConnectionString);
            services.AddSingleton<IAnalyticStore>(store);

         



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public MongoAnalyticStore GetAnalyticStore(string connectionString)
        {
            var store = (new MongoAnalyticStore(connectionString));
            return store;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<MongoDBSettings> settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Fixar Cultura para en-US
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new CultureInfo("en-IN") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-IN") },
                DefaultRequestCulture = new RequestCulture("en-IN")
            };

            app.UseRequestLocalization(localizationOptions);
            app.UseServerSideAnalytics(GetAnalyticStore(ConnectionString));
            //  app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        
        }
    }
}

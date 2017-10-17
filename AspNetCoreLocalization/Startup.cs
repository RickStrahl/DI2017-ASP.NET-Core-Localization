using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreLocalization
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
            services
                .AddLocalization(options => options.ResourcesPath = "Properties");

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

                // use a single shared ResourceSet instead            
                //.AddDataAnnotationsLocalization(opt =>
                //{
                //    opt.DataAnnotationLocalizerProvider = (type, factory) =>
                //        factory.Create(typeof(SharedViewModelValidations));
                //});
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            var cultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("de-DE"),
                new CultureInfo("de"),
                new CultureInfo("fr"),
                //new CultureInfo("zh"),
                //new CultureInfo("pl")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = cultures,
                SupportedUICultures = cultures
            });

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}

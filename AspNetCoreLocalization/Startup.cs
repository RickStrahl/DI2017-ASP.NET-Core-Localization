using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Westwind.Globalization;
using Westwind.Globalization.AspnetCore;

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
            services.AddSingleton(typeof(IStringLocalizerFactory),
                typeof(DbResStringLocalizerFactory));

            services.AddSingleton(typeof(IHtmlLocalizer),
                typeof(DbResHtmlLocalizerFactory));

            // Required: this enables West Wind Globalization
            services.AddWestwindGlobalization(opt =>
            {
                // the default settings comme from DbResourceConfiguration.json if exists
                // you can override the settings here, the config you create is added
                // to the DI system (DbResourceConfiguration)

                // Resource Mode - from Database (or Resx for serving from Resources)
                opt.ResourceAccessMode = ResourceAccessMode.DbResourceManager;  // ResourceAccessMode.Resx

                // Make sure the database you connect to exists
                opt.ConnectionString = "server=.;database=localizations;integrated security=true";

                // The table in which resources are stored
                opt.ResourceTableName = "localizations_DEVINTERSECTION_2";

                opt.AddMissingResources = false;
                opt.ResxBaseFolder = "~/Properties/";

                // Set up security for Localization Administration form
                opt.ConfigureAuthorizeLocalizationAdministration(actionContext =>
                {
                    // return true or false whether this request is authorized
                    return true;   //actionContext.HttpContext.User.Identity.IsAuthenticated;
                });

            });
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
                //new CultureInfo("de-DE"),
                new CultureInfo("de"),
                new CultureInfo("fr"),
                //new CultureInfo("zh"),
                new CultureInfo("pl")
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

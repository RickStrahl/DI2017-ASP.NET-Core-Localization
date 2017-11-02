### Dotnet Watch

```xml
<ItemGroup>
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
</ItemGroup>
```

### Localization Configuration

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddLocalization(opt => opt.ResourcesPath="Properties");

    services.AddMvc()
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();
}
```

Configure:

```cs
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("en"),
    new CultureInfo("de-DE"),
    new CultureInfo("de"),
    new CultureInfo("fr")
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
```

### Westwind Globalization Add Localization

Configuration `DResourceConfiguration.json` - make sure to `Copy if newer` is set

```json
"ConnectionString": "server=.;database=localizations;integrated security=yes",
"ResourceTableName": "Localizations_DevIntersection",
```  

Startup Configuration in `ConfigureServices`:

```cs
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
    opt.ResourceTableName = "localizations_DEVINTERSECTION";

    opt.AddMissingResources = false;
    opt.ResxBaseFolder = "~/Properties/";

    // Set up security for Localization Administration form
    opt.ConfigureAuthorizeLocalizationAdministration(actionContext =>
    {
        // return true or false whether this request is authorized
        return true;   //actionContext.HttpContext.User.Identity.IsAuthenticated;
    });

});
```         

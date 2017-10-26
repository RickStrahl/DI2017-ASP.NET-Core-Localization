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
services.AddSingleton(typeof(IStringLocalizerFactory), typeof(DbResStringLocalizerFactory));
services.AddSingleton(typeof(IHtmlLocalizer), typeof(DbResHtmlLocalizerFactory));

services.AddWestwindGlobalization();
```         

using ClientLibrary.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor;

namespace Client.Registrars;

public class SyncFusionRegistrar : IWebAssemblyHostBuilderRegistrar
{
    public void RegisterServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.Configure<SyncFusion>(builder.Configuration.GetSection("SyncFusion"));
        var syncFusionKeyBeta = builder.Configuration.GetSection(nameof(SyncFusion)).Get<SyncFusion>();

        // modify...
        if (!string.IsNullOrEmpty(syncFusionKeyBeta?.MyKey))
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncFusionKeyBeta?.MyKey);
        }
        else
        {
            throw new InvalidOperationException("SyncFusion license key is not configured.");
        }

        builder.Services.AddSyncfusionBlazor();
        builder.Services.AddScoped<SfDialogService>();
    }
}

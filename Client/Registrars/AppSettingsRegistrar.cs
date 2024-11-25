using Client.ApplicationStates;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client.Registrars;

public class AppSettingsRegistrar : IWebAssemblyHostBuilderRegistrar
{
    public void RegisterServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("blazor-emp-mgmt-backend.azurewebsites.net") });
        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        builder.Services.AddScoped<AllState>();

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
         
    }
}

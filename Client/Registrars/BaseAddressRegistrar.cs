using ClientLibrary.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Client.Registrars;

public class BaseAddressRegistrar : IWebAssemblyHostBuilderRegistrar
{
    public void RegisterServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient("SystemApiClient", client =>
        {
            var env = builder.HostEnvironment;

            if (env.IsDevelopment())
                client.BaseAddress = new Uri("https://localhost:7164");
            else if (env.IsStaging())
                client.BaseAddress = new Uri("https://blazor-emp-mgmt-backend.azurewebsites.net/");
            else
                client.BaseAddress = new Uri("https://blazor-emp-mgmt-backend.azurewebsites.net/");
        }).AddHttpMessageHandler<CustomHttpHandler>();
        


    }
}

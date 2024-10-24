using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client.Registrars;

public class AuthorizationRegistrar : IWebAssemblyHostBuilderRegistrar
{
    public void RegisterServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore();
    }
}

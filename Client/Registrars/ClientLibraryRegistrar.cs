using ClientLibrary;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client.Registrars;

public class ClientLibraryRegistrar : IWebAssemblyHostBuilderRegistrar
{
    public void RegisterServices(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddClientLibrary();
    }
}

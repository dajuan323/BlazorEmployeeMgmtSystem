
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client.Registrars;


public interface IWebAssemblyHostBuilderRegistrar : IRegistrar
{
    void RegisterServices(WebAssemblyHostBuilder builder);
}

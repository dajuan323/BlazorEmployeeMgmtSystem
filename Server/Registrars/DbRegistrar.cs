
using Microsoft.Identity.Client;
using ServerLibrary;

namespace Server.Registrars;

public class DbRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Host.ConfigureServices((hostContext, services) =>
        {
            services.AddServerLibrary(hostContext.Configuration);
        });
    }
}

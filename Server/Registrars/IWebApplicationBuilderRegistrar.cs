namespace Server.Registrars;

public interface IWebApplicationBuilderRegistrar : IRegistrar
{
    void RegisterServices(WebApplicationBuilder builder);
}

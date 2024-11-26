
namespace Server.Registrars;

public class CorsRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorWasm",
                builder => builder
                .WithOrigins(
                    "http://localhost:5104",
                    "https://localhost:7116", 
                    "https://salmon-moss-0c377bb0f.5.azurestaticapps.net")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
    }
}

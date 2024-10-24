
namespace Server.Registrars;

public class CorsRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorWasm",
                builder => builder
                .WithOrigins("http://localhost:5104", "https://localhost:7116")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
    }
}

using Server.Registrars;

namespace Server.Extensions;

public static class RegistrarExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
    {
        try
        {
            var registrars = GetRegistrars<IWebApplicationBuilderRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"An error occurred registering services: {ex.Message}");
            throw;
        }
    }

    public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
    {
        try
        {
            var registrars = GetRegistrars<IWebApplicationRegistrar>(scanningType);
            foreach(var registrar in registrars)
            {
                registrar.RegisterPipelineComponents(app);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while registering pipelines: {ex.Message}");
            throw;
        }

    }

    private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistrar
    {
        return scanningType.Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<T>();
    }

}

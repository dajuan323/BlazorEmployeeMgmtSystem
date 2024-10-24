using Client.Registrars;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.CompilerServices;

namespace Client.Extensions;

public static class RegistrarExtensions
{
    public static void RegisterServices(this WebAssemblyHostBuilder builder, Type scanningType)
    {
        try
        {
            var registrars = GetRegistrars<IWebAssemblyHostBuilderRegistrar>(scanningType);
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


    private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistrar
    {
        return scanningType.Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<T>();
    }
    
}

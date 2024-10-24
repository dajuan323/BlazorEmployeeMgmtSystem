using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BaseLibrary.Entities;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using ClientLibrary.Helpers;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientLibrary;

public static class DependencyInjection
{
    public static IServiceCollection AddClientLibrary(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        // General Department / Department / Branch
        services.AddScoped<IGenericServiceInterface<GeneralDepartment>, GenericServiceImplementation<GeneralDepartment>>();
        services.AddScoped<IGenericServiceInterface<Department>, GenericServiceImplementation<Department>>();
        services.AddScoped<IGenericServiceInterface<Branch>, GenericServiceImplementation<Branch>>();

        // Country / City / Town
        services.AddScoped<IGenericServiceInterface<Country>, GenericServiceImplementation<Country>>();
        services.AddScoped<IGenericServiceInterface<City>, GenericServiceImplementation<City>>();
        services.AddScoped<IGenericServiceInterface<Town>, GenericServiceImplementation<Town>>();

        services.AddScoped<IGenericServiceInterface<Employee>, GenericServiceImplementation<Employee>>();

        services.AddScoped<IUserAccountService, UserAccountService>();



        services.AddTransient<CustomHttpHandler>();

        services.AddScoped<LocalStorageService>();


        services.AddHttpClient("SystemApiClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7164");
        }).AddHttpMessageHandler<CustomHttpHandler>();



        services.AddScoped<GetHttpClient>();

        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();



        return services;
    }
}

using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary;

public static class DependencyInjection
{
    public static IServiceCollection AddServerLibrary(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default") ??
                throw new InvalidOperationException("Database not found."));
        });

        services.AddScoped<IUserAccount, UserAccountRepository>();

        services.AddScoped<IGenericRepositoryInterface<GeneralDepartment>, GeneralDepartmentRepository>();
        services.AddScoped<IGenericRepositoryInterface<Department>, DepartmentRepository>();
        services.AddScoped<IGenericRepositoryInterface<Branch>, BranchRepository>();

        services.AddScoped<IGenericRepositoryInterface<Country>, CountryRepository>();
        services.AddScoped<IGenericRepositoryInterface<City>, CityRepository>();
        services.AddScoped<IGenericRepositoryInterface<Town>, TownRepository>();

        services.AddScoped<IGenericRepositoryInterface<Employee>, EmployeeRepository>();

        return services;
    }
}

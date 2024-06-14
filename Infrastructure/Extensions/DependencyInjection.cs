using Application.Abstractions.FactoryInterfaces;
using Application.Abstractions.RepositoryInterfaces;
using Infrastructure.Factories;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddRepositoryDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DefaultDbConnectionFactory>();
        
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPassportRepository, PassportRepository>();
    }
}
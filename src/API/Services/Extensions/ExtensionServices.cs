using Abstraction.Repositories;
using Abstraction.Services;
using CodeExcercise.Common.Middleware;
using CodeExcercise.Common.Models.Domain;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.Common.Validators;
using FluentValidation;

namespace CodeExcercise.Services.Extensions;

/// <summary>
/// Extension to IServiceCollection
/// </summary>
public static class ExtensionServices
{
    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services"></param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<DatabaseCustomer>, CustomerRepository>();
        services.AddScoped<IValidationService<Customer>, ValidationCustomerService>();
        services.AddScoped<IMapService<Customer, DatabaseCustomer>, MapCustomerToDatabaseService>();

        services.AddScoped<AbstractValidator<Customer>, ModelValidator>();

        services.AddSingleton<ErrorHandlingMiddleware>();
    }
}
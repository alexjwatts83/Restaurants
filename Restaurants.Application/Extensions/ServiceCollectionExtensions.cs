using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services;
using System.Reflection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();

        var assembly = Assembly.GetExecutingAssembly();

        services.RegisterMapsterConfigurations(assembly);

        services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection RegisterMapsterConfigurations(this IServiceCollection services, Assembly assembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(assembly);

        services.AddSingleton(config);

        return services;
    }
}

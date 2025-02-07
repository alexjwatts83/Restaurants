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

        services.RegisterMapsterConfigurations();

        return services;
    }

    public static IServiceCollection RegisterMapsterConfigurations(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);

        return services;
    }
}

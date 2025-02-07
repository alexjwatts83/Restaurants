using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();

        return services;
    }
}

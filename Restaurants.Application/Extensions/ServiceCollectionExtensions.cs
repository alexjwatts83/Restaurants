using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Services;
using Restaurants.Domain.Entities;
using System.Reflection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();

        services.RegisterMapsterConfiguration();

        return services;
    }

    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Dish, DishDto>
            .NewConfig();

        TypeAdapterConfig<Restaurant, RestaurantDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.Address.City)
            .Map(dest => dest.Street, src => src.Address.Street)
            .Map(dest => dest.PostalCode, src => src.Address.PostalCode)
            .Map(dest => dest.Dishes, src => src.Dishes.Adapt<List<DishDto>>());

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") 
            ?? throw new InvalidOperationException("Database Connection string was not or empty");

        services.AddDbContext<RestaurantsDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

        return services;
    }
}


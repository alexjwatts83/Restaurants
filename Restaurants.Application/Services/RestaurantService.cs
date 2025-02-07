using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

public class RestaurantService(IRestaurantsRepository repository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<Restaurant?> GeByIdAsync(int id)
    {
        logger.LogInformation("Getting Restaurant by id '{Id}'", id);

        var entity = await repository.GeByIdAsync(id);

        return entity;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        logger.LogInformation("Getting all Restaurants");

        var entities = await repository.GetAllAsync();

        return entities;
    }
}

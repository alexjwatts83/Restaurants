using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

public class RestaurantService(IRestaurantsRepository repository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        logger.LogInformation("Getting all Restaurants");

        var entities = await repository.GetAllAsync();

        return entities;
    }
}

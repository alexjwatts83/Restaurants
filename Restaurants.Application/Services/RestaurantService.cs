using Mapster;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

public class RestaurantService(IRestaurantsRepository repository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<int> CreateAsync(Restaurant entity)
    {
        logger.LogInformation("Creating Restaurant {@Entity}", entity);

        var id = await repository.Create(entity);

        return id;
    }

    public async Task<RestaurantDto?> GeByIdAsync(int id)
    {
        logger.LogInformation("Getting Restaurant by id '{Id}'", id);

        var entity = await repository.GeByIdAsync(id);

        return entity.Adapt<RestaurantDto>();
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all Restaurants");

        var entities = await repository.GetAllAsync();

        return entities.Adapt<List<RestaurantDto>>();
    }
}

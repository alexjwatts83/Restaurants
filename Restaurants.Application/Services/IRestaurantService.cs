using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();

    Task<RestaurantDto?> GeByIdAsync(int id);

    Task<int> CreateAsync(Restaurant entity);
}
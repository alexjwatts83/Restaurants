using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();

    Task<RestaurantDto?> GeByIdAsync(int id);
}
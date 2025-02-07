using Restaurants.Domain.Entities;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAllAsync();

    Task<Restaurant?> GeByIdAsync(int id);
}
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> CreateAsync(Restaurant entity);
    Task DeleteAsync(Restaurant entity);
    Task UpdateAsync(Restaurant entity);
    Task<(IEnumerable<Restaurant> entities, int totalCount)> GetAllMatchingAsync(
        string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}

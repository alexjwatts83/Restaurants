using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<int> CreateAsync(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(Restaurant entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants.Include(x => x.Dishes).FirstOrDefaultAsync(x => x.Id == id);

        return restaurant;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.Include(x => x.Dishes).ToListAsync();
        return restaurants;
    }

    public async Task UpdateAsync(Restaurant entity)
    {
        dbContext.Restaurants.Update(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<(IEnumerable<Restaurant> entities, int totalCount)> GetAllMatchingAsync(string? searchPhrase,
       int pageSize,
       int pageNumber,
       string? sortBy,
       SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                   || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }
}

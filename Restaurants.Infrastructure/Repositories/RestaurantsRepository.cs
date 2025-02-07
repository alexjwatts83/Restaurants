using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<(bool, string?)> DeleteByIdAsync(int id)
    {
        var entity = await dbContext.Restaurants.FindAsync(id);

        if (entity == null)
            return (false, $"Restaurant with '{id}' doesnt exists");

        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();

        return (true, null);
    }

    public async Task<Restaurant?> GeByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants.Include(x => x.Dishes).FirstOrDefaultAsync(x => x.Id == id);

        return restaurant;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.Include(x => x.Dishes).ToListAsync();
        return restaurants;
    }
}

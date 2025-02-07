using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<Restaurant?> GeByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

        return restaurant;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }
}

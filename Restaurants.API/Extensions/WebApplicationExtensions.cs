using Restaurants.Infrastructure.Seeders;

namespace Restaurants.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

            await seeder.Seed();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

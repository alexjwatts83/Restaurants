using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext, UserManager<AppUser> userManager)
    : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
            await dbContext.Database.MigrateAsync();

        if (!await dbContext.Database.CanConnectAsync())
            return;

        if (!dbContext.Restaurants.Any())
            await SeedRestaurants(dbContext);

        if (!dbContext.Roles.Any())
            await SeedRoles(dbContext);

        if (!dbContext.Users.Any())
            await SeedUsers(userManager);
    }

    private static async Task SeedRestaurants(RestaurantsDbContext dbContext)
    {
        var entities = GetRestaurants();
        dbContext.Restaurants.AddRange(entities);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedRoles(RestaurantsDbContext dbContext)
    {
        var entities = GetRoles();
        dbContext.Roles.AddRange(entities);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUsers(UserManager<AppUser> userManager)
    {
        await SeedUser(userManager, "admin@test.com", UserRoles.Admin);
        await SeedUser(userManager, "owner@test.com", UserRoles.Owner);
        await SeedUser(userManager, "ckent@test.com", UserRoles.User);
    }

    private static async Task SeedUser(UserManager<AppUser> userManager, string email, string role)
    {
        if (userManager.Users.Any(x => x.UserName == email))
            return;

        var user = new AppUser
        {
            Email = email,
            UserName = email
        };

        var result = await userManager.CreateAsync(user, "Pa$$wOrd321");

        if (!result.Succeeded)
            throw new InvalidOperationException("User Failed to create");

        await userManager.AddToRoleAsync(user, role);
    }

    private static IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];

        return roles;
    }

    private static List<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, " +
                    "that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },

            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];

        return restaurants;
    }
}
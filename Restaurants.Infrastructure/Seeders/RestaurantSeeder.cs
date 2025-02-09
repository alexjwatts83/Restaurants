using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System.Data;

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

        if (!dbContext.Roles.Any())
            await SeedRoles(dbContext);

        if (!dbContext.Users.Any())
            await SeedUsers(userManager);

        if (!dbContext.Restaurants.Any())
        {
            var user = await userManager.FindByEmailAsync("owner@test.com");
            await SeedRestaurants(dbContext, user!);
        }
    }

    private static async Task SeedRestaurants(RestaurantsDbContext dbContext, AppUser owner)
    {
        var entities = GetRestaurants(owner);
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
        await SeedUser(userManager, "admin@test.com", null, null, UserRoles.Admin);
        await SeedUser(userManager, "owner@test.com", null, null, UserRoles.Owner);
        await SeedUser(userManager, "ckent@test.com", null, null, UserRoles.User);
        await SeedUser(userManager, "test@test.com", new DateOnly(1980, 1, 7), "American", UserRoles.User, "Role1");
        await SeedUser(userManager, "german@test.com", new DateOnly(1985, 1, 7), "German", UserRoles.User);
        await SeedUser(userManager, "polish@test.com", new DateOnly(1983, 1, 7), "Polish", UserRoles.User);
        await SeedUser(userManager, "young@test.com", new DateOnly(2015, 1, 7), null, UserRoles.User);

        foreach (var i in Enumerable.Range(1, 5))
            await SeedUser(userManager, $"owner{i}@test.com", null, null, UserRoles.Owner);
    }

    private static async Task SeedUser(UserManager<AppUser> userManager, string email, DateOnly? dateOfBirth, string? nationality, params string[] roles)
    {
        if (userManager.Users.Any(x => x.UserName == email))
            return;

        var user = new AppUser
        {
            Email = email,
            UserName = email,
            DateOfBirth = dateOfBirth,
            Nationality = nationality,
        };

        var result = await userManager.CreateAsync(user, "Pa$$wOrd321");

        if (!result.Succeeded)
            throw new InvalidOperationException("User Failed to create");

        foreach (var role in roles)
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

        var moreRoles = Enumerable
            .Range(1, 9)
            .Select(x =>
            {
                var roleName = $"Role{x}";
                return new IdentityRole(roleName)
                {
                    NormalizedName = roleName.ToUpper()
                };
            })
            .ToList();

        roles.AddRange(moreRoles);

        return roles;
    }

    private static List<Restaurant> GetRestaurants(AppUser owner)
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
                OwnerId = owner.Id
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
                },
                OwnerId = owner.Id
            }
        ];

        return restaurants;
    }
}
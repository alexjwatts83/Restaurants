using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Identity;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;
public class RestaurantAuthorizationService(IUserContext userContext, ILogger<RestaurantAuthorizationService> logger)
    : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
            user!.Email,
            resourceOperation,
            restaurant.Name);

        if (resourceOperation == ResourceOperation.Read)
        {
            logger.LogInformation("Read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Create && user.IsInRole(UserRoles.Owner))
        {
            logger.LogInformation("Create operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }
}

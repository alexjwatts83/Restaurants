namespace Restaurants.Domain.Identity;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

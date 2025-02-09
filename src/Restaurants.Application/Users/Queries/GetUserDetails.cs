namespace Restaurants.Application.Users.Queries;

public class GetUserDetailsQuery : IQuery<CurrentUser>
{
}

public class GetUserDetailsQueryHandler(
    IUserContext userContext,
    ILogger<GetUserDetailsQueryHandler> logger
) : IQueryHandler<GetUserDetailsQuery, CurrentUser>
{
    public async Task<CurrentUser> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting current user details");

        var user = userContext.GetCurrentUser();

        if (user == null)
            throw new NotFoundException(nameof(CurrentUser), user!.Id);

        return user;
    }
}

using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Identity;

namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommand : ICommand
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}

public class UpdateUserDetailsCommandHandler(IUserContext userContext,
    IUserStore<AppUser> userStore,
    ILogger<UpdateUserDetailsCommandHandler> logger)
    : ICommandHandler<UpdateUserDetailsCommand>
{
    public async Task<Unit> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        if (dbUser == null)
            throw new NotFoundException(nameof(AppUser), user!.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);

        return Unit.Value;
    }
}
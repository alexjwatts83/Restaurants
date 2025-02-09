namespace Restaurants.Application.Users.Commands;

public class UnassignUserRoleCommand : ICommand
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}

public class UnassignUserRoleCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ILogger<UnassignUserRoleCommandHandler> logger)
    : ICommandHandler<UnassignUserRoleCommand>
{
    public async Task<Unit> Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassigning user role: {@Request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(AppUser), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);

        return Unit.Value;
    }
}
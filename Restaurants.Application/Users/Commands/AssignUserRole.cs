namespace Restaurants.Application.Users.Commands;

public class AssignUserRoleCommand : ICommand
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}

public class AssignUserRoleCommanddHandler(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ILogger<AssignUserRoleCommanddHandler> logger)
    : ICommandHandler<AssignUserRoleCommand>
{
    public async Task<Unit> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user role: {@Request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(AppUser), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.AddToRoleAsync(user, role.Name!);

        return Unit.Value;
    }
}
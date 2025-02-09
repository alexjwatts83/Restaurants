namespace Restaurants.Application.Restaurants.Commands;

public class DeleteRestaurantCommand(int id) : ICommand
{
    public int Id { get; } = id;
}

public class DeleteRestaurantCommandHandler(
    IRestaurantsRepository repository,
    IRestaurantAuthorizationService authService,
    ILogger<DeleteRestaurantCommandHandler> logger)
    : ICommandHandler<DeleteRestaurantCommand>
{
    public async Task<Unit> Handle(DeleteRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id: {RestaurantId}", command.Id);

        var restaurant = await repository.GetByIdAsync(command.Id) 
            ?? throw new NotFoundException(nameof(Restaurant), command.Id.ToString());

        if (!authService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();

        await repository.DeleteAsync(restaurant);

        return Unit.Value;
    }
}
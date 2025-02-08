namespace Restaurants.Application.Restaurants.Commands;

public class DeleteRestaurantCommand(int id) : ICommand
{
    public int Id { get; } = id;
}

public class DeleteRestaurantCommandHandler(IRestaurantsRepository repository, ILogger<DeleteRestaurantCommandHandler> logger)
    : ICommandHandler<DeleteRestaurantCommand>
{
    public async Task<Unit> Handle(DeleteRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id: {RestaurantId}", command.Id);

        var restaurant = await repository.GetByIdAsync(command.Id) ?? throw new NotFoundException(nameof(Restaurant), command.Id.ToString());

        await repository.DeleteByIdAsync(restaurant);

        return Unit.Value;
    }
}
using MapsterMapper;
using MediatR;

namespace Restaurants.Application.Restaurants.Commands;

public class UpdateRestaurantCommand : ICommand
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
}

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 100);
    }
}

public class UpdateRestaurantCommandHandler(
    IRestaurantsRepository repository,
    IRestaurantAuthorizationService authService,
    IMapper mapper,
    ILogger<UpdateRestaurantCommandHandler> logger)
        : ICommandHandler<UpdateRestaurantCommand>
{
    public async Task<Unit> Handle(UpdateRestaurantCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", command.Id, command);

        var restaurant = await repository.GetByIdAsync(command.Id);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), command.Id.ToString());

        if (!authService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(command, restaurant);

        await repository.UpdateAsync(restaurant);

        return Unit.Value;
    }
}
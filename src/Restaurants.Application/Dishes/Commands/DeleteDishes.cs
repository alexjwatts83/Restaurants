﻿namespace Restaurants.Application.Dishes.Commands;

public class DeleteDishesForRestaurantCommand(int restaurantId) : ICommand
{
    public int RestaurantId { get; } = restaurantId;
}

public class DeleteDishesForRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository repository,
    IRestaurantAuthorizationService authService,
    ILogger<DeleteDishesForRestaurantCommandHandler> logger) 
        : ICommandHandler<DeleteDishesForRestaurantCommand>
{
    public async Task<Unit> Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!authService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();

        await repository.DeleteAsync(restaurant.Dishes);

        return Unit.Value;
    }
}
namespace Restaurants.Application.Dishes.Queries;

public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId) : IQuery<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}

public class GetDishByIdForRestaurantQueryHandler(IRestaurantsRepository repository, ILogger<GetDishByIdForRestaurantQueryHandler> logger)
    : IQueryHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}",
            request.DishId,
            request.RestaurantId);

        var restaurant = await repository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish == null) 
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var result = dish.Adapt<DishDto>();

        return result;
    }
}
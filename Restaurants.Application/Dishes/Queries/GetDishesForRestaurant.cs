namespace Restaurants.Application.Dishes.Queries;

public class GetDishesForRestaurantQuery(int restaurantId) : IQuery<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}

public class GetDishesForRestaurantQueryHanlder(IRestaurantsRepository repository, ILogger<GetDishesForRestaurantQueryHanlder> logger)
    : IQueryHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}", request.RestaurantId);

        var restaurant = await repository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var results = restaurant.Dishes.Adapt<IEnumerable<DishDto>>();

        return results;
    }
}
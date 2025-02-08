namespace Restaurants.Application.Restaurants.Queries;

public class GetAllRestaurantsQuery : IQuery<IEnumerable<RestaurantDto>>
{
}

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository repository, ILogger<CreateRestaurantCommandHandler> logger) 
    : IQueryHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Restaurants");

        var entities = await repository.GetAllAsync();

        return entities.Adapt<List<RestaurantDto>>();
    }
}

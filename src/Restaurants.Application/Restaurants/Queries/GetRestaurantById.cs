namespace Restaurants.Application.Restaurants.Queries;

public class GetRestaurantByIdQuery(int id) : IQuery<RestaurantDto>
{
    public int Id { get; private set; } = id;
}

public class GetRestaurantByIdQueryHandler(IRestaurantsRepository repository, ILogger<GetRestaurantByIdQueryHandler> logger)
    : IQueryHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting Restaurant by id '{request.Id}'");

        var entity = await repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString()); ;

        return entity.Adapt<RestaurantDto>();
    }
}

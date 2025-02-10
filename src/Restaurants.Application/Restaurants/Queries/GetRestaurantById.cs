namespace Restaurants.Application.Restaurants.Queries;

public class GetRestaurantByIdQuery(int id) : IQuery<RestaurantDto>
{
    public int Id { get; private set; } = id;
}

public class GetRestaurantByIdQueryHandler(
    IRestaurantsRepository repository,
    IBlobStorageService blobStorageService,
    ILogger<GetRestaurantByIdQueryHandler> logger)
        : IQueryHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting Restaurant by id '{request.Id}'");

        var entity = await repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString()); ;

        var dto = entity.Adapt<RestaurantDto>();

        dto.LogoSasUrl = blobStorageService.GetBlobSasUrl(entity.LogoUrl);

        return dto;
    }
}

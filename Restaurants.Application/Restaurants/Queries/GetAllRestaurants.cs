namespace Restaurants.Application.Restaurants.Queries;

public class GetAllRestaurantsQuery : IQuery<PagedResult<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] allowPageSizes = [5, 10, 15, 30];
    private readonly string[] allowedSortByColumnNames = 
    [
        nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description)
    ];


    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository repository, ILogger<GetAllRestaurantsQueryHandler> logger)
    : IQueryHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Restaurants");

        var (entities, totalCount) = await repository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var dtos = entities.Adapt<IEnumerable<RestaurantDto>>();

        var result = new PagedResult<RestaurantDto>(dtos, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}

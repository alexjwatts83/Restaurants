namespace Restaurants.Application.Restaurants.Commands;

public class CreateRestaurantCommand : ICommand<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
           .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty();

        RuleFor(dto => dto.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid category. Please choose from the valid categories.");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.ContactNumber)
            .NotEmpty();


        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");
    }
}

public class CreateRestaurantCommandHandler(
    IRestaurantsRepository repository,
    IUserContext userContext,
    ILogger<CreateRestaurantCommandHandler> logger)
        : ICommandHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} ('{UserId}') is creating restaurant {@Request}", user!.Email, user.Id, request);

        var entity = request.Adapt<Restaurant>();

        entity.OwnerId = user.Id;

        var id = await repository.CreateAsync(entity);

        return id;
    }
}
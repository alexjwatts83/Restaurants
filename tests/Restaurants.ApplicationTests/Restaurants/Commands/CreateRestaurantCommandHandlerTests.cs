namespace Restaurants.Application.Restaurants.Commands.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
        };
        var restaurant = command.Adapt<Restaurant>();

        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var authService = new Mock<IRestaurantAuthorizationService>();
        authService.Setup(m => m.Authorize(restaurant, ResourceOperation.Create))
            .Returns(true);

        var commandHandler = new CreateRestaurantCommandHandler(
            restaurantRepositoryMock.Object,
            userContextMock.Object,
            authService.Object,
            mapperMock.Object,
            loggerMock.Object
        );

        // act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert
        result.ShouldBe(1);
        restaurant.OwnerId.ShouldBe("owner-id");
        restaurant.Name.ShouldBe(command.Name);
        restaurantRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);
    }
}
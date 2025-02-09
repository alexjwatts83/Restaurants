namespace Restaurants.Application.Restaurants.Dtos.Tests;

public class RestaurantMappingConfigTests
{
    public RestaurantMappingConfigTests()
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(RestaurantMappingConfig).Assembly);
    }

    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        // arrange
        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123456789",
            Address = new Address
            {
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345"
            }
        };

        // act
        var restaurantDto = restaurant.Adapt<RestaurantDto>();

        // assert 
        restaurantDto.ShouldNotBeNull();
        restaurantDto.Id.ShouldBe(restaurant.Id);
        restaurantDto.Name.ShouldBe(restaurant.Name);
        restaurantDto.Description.ShouldBe(restaurant.Description);
        restaurantDto.Category.ShouldBe(restaurant.Category);
        restaurantDto.HasDelivery.ShouldBe(restaurant.HasDelivery);
        restaurantDto.City.ShouldBe(restaurant.Address.City);
        restaurantDto.Street.ShouldBe(restaurant.Address.Street);
        restaurantDto.PostalCode.ShouldBe(restaurant.Address.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = false
        };

        // act
        var restaurant = command.Adapt<Restaurant>();

        // assert 
        restaurant.ShouldNotBeNull();
        restaurant.Id.ShouldBe(command.Id);
        restaurant.Name.ShouldBe(command.Name);
        restaurant.Description.ShouldBe(command.Description);
        restaurant.HasDelivery.ShouldBe(command.HasDelivery);
    }

    [Fact()]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123456789",
            City = "Test City",
            Street = "Test Street",
            PostalCode = "12345"
        };

        // act
        var restaurant = command.Adapt<Restaurant>();

        // assert 
        restaurant.ShouldNotBeNull();
        restaurant.Name.ShouldBe(command.Name);
        restaurant.Description.ShouldBe(command.Description);
        restaurant.Category.ShouldBe(command.Category);
        restaurant.HasDelivery.ShouldBe(command.HasDelivery);
        restaurant.ContactEmail.ShouldBe(command.ContactEmail);
        restaurant.ContactNumber.ShouldBe(command.ContactNumber);
        restaurant.Address.ShouldNotBeNull();
        restaurant.Address.City.ShouldBe(command.City);
        restaurant.Address.Street.ShouldBe(command.Street);
        restaurant.Address.PostalCode.ShouldBe(command.PostalCode);
    }
}
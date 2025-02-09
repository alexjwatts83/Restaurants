using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Seeders;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Shouldly;
using System.Net.Http.Json;
using System.Net;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();
    private readonly Mock<IRestaurantSeeder> _restaurantsSeederMock = new();

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                                            _ => _restaurantsRepositoryMock.Object));


                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantSeeder),
                                            _ => _restaurantsSeederMock.Object));
            });
        });
    }

    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // arrange
        var id = 1123;

        _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // arrange
        var id = 99;

        var restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test",
            Description = "Test description"
        };

        _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        restaurantDto.ShouldNotBeNull();
        restaurantDto.Name.ShouldBe("Test");
        restaurantDto.Description.ShouldBe("Test description");
    }

    [Fact]
    public async Task GetAll_ForValidRequest_Returns200Ok()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

        // assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);

    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var result = await client.GetAsync("/api/restaurants?sortBy=invalid");

        // assert
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    //[Fact()]
    //public void CreateAsyncTest()
    //{
    //    Xunit.assert.Fail("This test needs an implementation");
    //}

    //[Fact()]
    //public void UpdateRestaurantTest()
    //{
    //    Xunit.assert.Fail("This test needs an implementation");
    //}

    //[Fact()]
    //public void DeleteByIdAsyncTest()
    //{
    //    Xunit.assert.Fail("This test needs an implementation");
    //}
}
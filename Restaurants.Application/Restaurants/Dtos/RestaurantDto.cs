using Mapster;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDto> Dishes { get; set; } = [];
}

public class RestaurantMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Restaurant, RestaurantDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.Address.City)
            .Map(dest => dest.Street, src => src.Address.Street)
            .Map(dest => dest.PostalCode, src => src.Address.PostalCode)
            .Map(dest => dest.Dishes, src => src.Dishes.Adapt<List<DishDto>>());
    }
}
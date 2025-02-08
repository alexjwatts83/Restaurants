using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Commands;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Restaurant, RestaurantDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.Address == null ? null : src.Address.City)
            .Map(dest => dest.Street, src => src.Address == null ? null : src.Address.Street)
            .Map(dest => dest.PostalCode, src => src.Address == null ? null : src.Address.PostalCode)
            .Map(dest => dest.Dishes, src => src.Dishes.Adapt<List<DishDto>>());

        TypeAdapterConfig<CreateRestaurantCommand, Restaurant>
            .NewConfig()
            .Map(dest => dest.Address, src => new Address()
            {
                City = src.City,
                PostalCode = src.PostalCode,
                Street = src.Street,
            })
            .Map(dest => dest.Dishes, src => new List<Dish>());
    }
}
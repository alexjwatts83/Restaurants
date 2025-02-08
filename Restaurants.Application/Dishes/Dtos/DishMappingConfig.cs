namespace Restaurants.Application.Dishes.Dtos;

public class DishMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Dish, DishDto>
            .NewConfig();
    }
}

using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Services;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await restaurantService.GetAllAsync();

        return Ok(entities);
    }
}

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await restaurantService.GeByIdAsync(id);

        if (entity == null)
            return NotFound($"Restaurant of Id '{id}' not found");

        return Ok(entity);
    }
}

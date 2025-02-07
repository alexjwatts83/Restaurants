using Mapster;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Services;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
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

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRestaurantDto request)
    {
        var entity = request.Adapt<Restaurant>();
        var id = await restaurantService.CreateAsync(request);

        if (id <= 0)
            return BadRequest("Not able to create Restaurant");

        return CreatedAtAction(
             nameof(GetById),
             new { id },
             entity.Adapt<RestaurantDto>()
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        var (successful, errorMessage) = await restaurantService.DeleteByIdAsync(id);

        if (!successful)
            return NotFound(errorMessage);

        return NoContent();
    }
}

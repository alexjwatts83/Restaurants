﻿namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var request = new GetAllRestaurantsQuery();
        var entities = await mediator.Send(request);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateRestaurantCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));

        return NoContent();
    }
}

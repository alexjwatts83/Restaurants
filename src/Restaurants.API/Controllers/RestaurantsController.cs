﻿using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController(IMediator mediator/*, IUserContext userContext*/) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    //[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllAsync([FromQuery] GetAllRestaurantsQuery request)
    {
        //var user  = userContext.GetCurrentUser();

        //var request = new GetAllRestaurantsQuery();

        var entities = await mediator.Send(request);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        return Ok(restaurant);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateAsync(CreateRestaurantCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));

        return NoContent();
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var command = new UploadRestaurantLogoCommand()
        {
            RestaurantId = id,
            FileName = $"{id}-{file.FileName}",
            File = stream
        };

        await mediator.Send(command);

        return NoContent();
    }
}

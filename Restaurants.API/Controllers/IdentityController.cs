namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<ActionResult<CurrentUser?>> GetUserDetails()
    {
        var query = new GetUserDetailsQuery();

        var user = await mediator.Send(query);

        return Ok(user);
    }
}

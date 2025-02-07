using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Interfaces;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(IWeatherForecastService service) : ControllerBase
{
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return service.Get();
    }
}

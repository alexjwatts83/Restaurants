namespace Restaurants.API.Interfaces;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
}

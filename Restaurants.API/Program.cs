using Restaurants.API.Extensions;
using Restaurants.API.Interfaces;
using Restaurants.API.Services;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.InitialiseDatabaseAsync();

app.Run();

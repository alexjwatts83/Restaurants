using Restaurants.API.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.InitialiseDatabaseAsync();

app.Run();

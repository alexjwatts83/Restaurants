using Restaurants.API.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentation(builder.Host);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.BuildRequestPipeLine();

await app.InitialiseDatabaseAsync();

app.Run();

using Restaurants.API.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;
using Serilog;

try
{
    Log.Warning("Building App - Start");

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

    Log.Warning("Building App - Complete");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
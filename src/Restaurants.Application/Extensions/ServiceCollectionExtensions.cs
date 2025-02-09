using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.RegisterMapsterConfigurations(assembly);

        services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }

    public static IServiceCollection RegisterMapsterConfigurations(this IServiceCollection services, Assembly assembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(assembly);

        services.AddSingleton(config);

        var mapperConfig = new Mapper(config);

        services.AddSingleton<IMapper>(mapperConfig);

        return services;
    }
}

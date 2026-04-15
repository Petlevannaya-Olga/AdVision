using AdVision.Application;
using AdVision.Application.Generators;
using AdVision.Infrastructure.Generators;
using AdVision.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite("Data Source=AdVision.db");
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
            options.UseLoggerFactory(CreateLoggerFactory()); // только для dev
        });
        
        // Сидеры
        services.AddScoped<ISeeder, VenueTypesSeeder>();
        
        // Репозитории
        services.AddSingleton<IVenueTypeRepository, VenueTypeRepository>();
        services.AddSingleton<IVenueRepository, VenueRepository>();
        services.AddSingleton<ITariffRepository, TariffRepository>();
        services.AddSingleton<IPositionRepository, PositionRepository>();
        
        // Генераторы
        services.AddTransient<IVenueNameFakeGenerator, VenueNameFakeGenerator>();
        services.AddTransient<IVenueAddressFakeGenerator, VenueAddressFakeGenerator>();
        services.AddTransient<IVenueSizeFakeGenerator, VenueSizeFakeGenerator>();
        services.AddTransient<IVenueDescriptionFakeGenerator, VenueDescriptionFakeGenerator>();
        services.AddTransient<IVenueRatingFakeGenerator, VenueRatingFakeGenerator>();
        services.AddTransient<IVenueFakeGenerator, VenueFakeGenerator>();

        return services;
    }
    
    private static ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}
using AdVision.Application;
using Microsoft.Extensions.DependencyInjection;

namespace AdVision.Infrastructure.Seeding;

public static class SeederExtensions
{
    public static async Task<IServiceProvider> RunSeeders(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        
        var seeders = scope.ServiceProvider.GetServices<ISeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }

        return services;
    }
}
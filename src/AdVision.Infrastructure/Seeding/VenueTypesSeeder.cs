using AdVision.Application;
using AdVision.Domain.VenueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Seeding;

public class VenueTypesSeeder(ApplicationDbContext dbContext, ILogger<VenueTypesSeeder> logger) : ISeeder
{
    private static readonly string[] DefaultVenueTypeNames =
    [
        "Билборд",
        "Суперсайт",
        "Ситиборд",
        "Видеоэкран",
        "Медиафасад",
        "Остановка",
        "Реклама в метро",
        "Брендмауэр",
        "Пиллар",
        "Призма"
    ];
    
    public async Task SeedAsync()
    {
        logger.LogInformation("Началось сидирование типов площадок");
        try
        {
            await SeedDataAsync();
        }
        catch (Exception e)
        {
            logger.LogError("Произошла ошибка во время сидирования данных: {Message}", e.Message);
        }

        logger.LogInformation("Сидирование типов площадок завершено");
    }

    private async Task SeedDataAsync()
    {
        var existingNames = await dbContext.VenueTypes
            .AsNoTracking()
            .Select(x => x.Name.Value)
            .ToListAsync();

        var existingNamesSet = existingNames
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var venueTypesToAdd = new List<VenueType>();

        foreach (var rawName in DefaultVenueTypeNames)
        {
            var name = rawName.Trim();

            if (existingNamesSet.Contains(name))
            {
                continue;
            }

            var venueTypeNameResult = VenueTypeName.Create(name);
            if (venueTypeNameResult.IsFailure)
            {
                var errors = string.Join(
                    Environment.NewLine,
                    venueTypeNameResult.Error);

                logger.LogWarning(
                    "Тип площадки '{VenueTypeName}' пропущен из-за ошибки валидации: {Errors}",
                    name,
                    errors);

                continue;
            }

            var venueType = new VenueType(venueTypeNameResult.Value);

            venueTypesToAdd.Add(venueType);
        }

        if (venueTypesToAdd.Count == 0)
        {
            logger.LogInformation("Новые типы площадок для сидирования не найдены");
            return;
        }

        await dbContext.VenueTypes.AddRangeAsync(venueTypesToAdd);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Добавлено {Count} типов площадок",
            venueTypesToAdd.Count);
    }
}
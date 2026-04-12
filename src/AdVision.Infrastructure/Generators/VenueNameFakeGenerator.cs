using AdVision.Application.Generators;
using AdVision.Domain.Venues;
using Bogus;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators;

public class VenueNameFakeGenerator(ILogger<VenueNameFakeGenerator> logger) : IVenueNameFakeGenerator
{
    private readonly Faker _faker = new("ru");

    private static readonly string[] Suffixes =
    [
        "Media", "Digital", "Outdoor", "Billboard", "Screen",
        "Медиа", "Диджитал", "Аутдор", "Экран", "Реклама"
    ];

    private static readonly string[] Formats =
    [
        "{Company} {Type} {Suffix}",
        "{Company} {City} {Street} {Suffix}",
        "{Company} {City} {Suffix}",
        "{Company} {City} {Type} {Number}"
    ];

    public VenueName Generate(string type, string city, string street)
    {
        while (true)
        {
            var raw = GenerateRawName(_faker, type, city, street);
            var result = VenueName.Create(raw);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug("Сгенерировано невалидное название: {Raw}", raw);
        }
    }

    private static string GenerateRawName(Faker f, string type, string city, string street)
    {
        var format = f.PickRandom(Formats);

        var company = f.Company.CompanyName();
        var suffix = f.PickRandom(Suffixes);
        var number = f.Random.Int(1, 999);

        return format
            .Replace("{Company}", company)
            .Replace("{City}", city)
            .Replace("{Street}", street)
            .Replace("{Suffix}", suffix)
            .Replace("{Type}", type)
            .Replace("{Number}", number.ToString())
            .Trim();
    }

    private static string EnsureMinLength(string value, int minLength, Faker f)
    {
        return value.Length >= minLength ? value : $"{value} {f.Address.CitySuffix()}".Trim();
    }
}
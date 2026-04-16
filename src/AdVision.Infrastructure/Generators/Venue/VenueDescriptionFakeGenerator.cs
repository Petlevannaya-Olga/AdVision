using AdVision.Application.Generators;
using AdVision.Application.Generators.Venues;
using AdVision.Domain.Venues;
using Bogus;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators.Venue;

public class VenueDescriptionFakeGenerator(ILogger<VenueDescriptionFakeGenerator> logger)
    : IVenueDescriptionFakeGenerator
{
    private readonly Faker _faker = new("ru");

    public VenueDescription Generate(string typeName)
    {
        while (true)
        {
            var raw = GenerateRawDescription(typeName);
            var result = VenueDescription.Create(raw);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug("Сгенерировано невалидное описание: {Raw}", raw);
        }
    }

    private string GenerateRawDescription(string typeName)
    {
        var traffic = _faker.PickRandom(
            "с высоким автомобильным трафиком",
            "с высоким пешеходным трафиком",
            "в зоне активного городского движения",
            "вблизи крупных транспортных узлов");

        var placement = _faker.PickRandom(
            "расположен рядом с торговым центром",
            "расположен в деловом районе",
            "установлен возле крупной магистрали",
            "находится рядом с жилым массивом",
            "размещён в центральной части города");

        var visibility = _faker.PickRandom(
            "обеспечивает хорошую видимость рекламного сообщения",
            "подходит для длительных имиджевых кампаний",
            "эффективен для охватного размещения",
            "подходит для наружной и цифровой рекламы");

        return $"{typeName} {placement}, {traffic}. Объект {visibility}. " +
               $"Площадка подходит для размещения федеральных и локальных рекламных кампаний, " +
               $"а также для продвижения брендов, услуг и специальных предложений.";
    }
}
using AdVision.Application.Generators;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using Bogus;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators;

public class VenueAddressFakeGenerator(ILogger<VenueAddressFakeGenerator> logger) : IVenueAddressFakeGenerator
{
    private readonly Faker _faker = new("ru");

    public VenueAddress Generate()
    {
        var district = _faker.PickRandom(
            "Центральный район",
            "Ленинский район",
            "Советский район",
            "Октябрьский район",
            "Промышленный район",
            "Кировский район",
            "Железнодорожный район",
            "Индустриальный район",
            "Комсомольский район",
            "Первомайский район",
            "Фрунзенский район",
            "Красногвардейский район",
            "Красноармейский район",
            "Калининский район",
            "Московский район",
            "Невский район",
            "Свердловский район",
            "Дзержинский район",
            "Ворошиловский район",
            "Тракторозаводский район",
            "Заводской район",
            "Левобережный район",
            "Правобережный район",
            "Южный район",
            "Северный район",
            "Западный район",
            "Восточный район",
            "Пригородный район",
            "Нагорный район",
            "Приморский район"
        );
        
        while (true)
        {
            var dto = new AddressDto(
                Region: _faker.Address.State(),
                District: district,
                City: _faker.Address.City(),
                Street: _faker.Address.StreetName(),
                House: _faker.Address.BuildingNumber(),
                Latitude: GenerateLatitude(),
                Longitude: GenerateLongitude());

            var result = VenueAddress.Create(dto);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерирован невалидный адрес: {@AddressDto}",
                dto);
        }
    }

    private double GenerateLatitude()
    {
        return _faker.Random.Double(
            VenueAddress.MIN_LATITUDE_VALUE + 0.000001,
            VenueAddress.MAX_LATITUDE_VALUE - 0.000001);
    }

    private double GenerateLongitude()
    {
        return _faker.Random.Double(
            VenueAddress.MIN_LONGITUDE_VALUE + 0.000001,
            VenueAddress.MAX_LONGITUDE_VALUE - 0.000001);
    }
}
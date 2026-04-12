using AdVision.Application.Generators;
using AdVision.Domain.Venues;
using Bogus;

namespace AdVision.Infrastructure.Generators;

public class VenueSizeFakeGenerator : IVenueSizeFakeGenerator
{
    private readonly Faker _faker = new("ru");

    public VenueSize Generate()
    {
        while (true)
        {
            var width = _faker.Random.Double(VenueSize.MIN_WIDTH, 8_000);
            var height = _faker.Random.Double(VenueSize.MIN_HEIGHT, 8_000);

            var result = VenueSize.Create(width, height);
            if (result.IsSuccess)
                return result.Value;
        }
    }
}
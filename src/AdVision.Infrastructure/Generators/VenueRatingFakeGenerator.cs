using AdVision.Application.Generators;
using AdVision.Domain.Venues;
using Bogus;

namespace AdVision.Infrastructure.Generators;

public class VenueRatingFakeGenerator : IVenueRatingFakeGenerator
{
    private readonly Faker _faker = new("ru");

    public VenueRating Generate()
    {
        while (true)
        {
            var value = Math.Round(
                _faker.Random.Double(VenueRating.MIN, VenueRating.MAX), 
                1);

            var result = VenueRating.Create(value);

            if (result.IsSuccess)
                return result.Value;
        }
    }
}
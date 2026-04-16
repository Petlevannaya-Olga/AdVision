using AdVision.Application.Generators;
using AdVision.Application.Generators.Venues;
using AdVision.Domain.VenueTypes;

namespace AdVision.Infrastructure.Generators.Venue;

public class VenueFakeGenerator(
    IVenueNameFakeGenerator nameFakeGenerator,
    IVenueAddressFakeGenerator addressFakeGenerator,
    IVenueSizeFakeGenerator sizeFakeGenerator,
    IVenueRatingFakeGenerator ratingFakeGenerator,
    IVenueDescriptionFakeGenerator descriptionFakeGenerator) : IVenueFakeGenerator
{
    public Domain.Venues.Venue Generate(string typeName)
    {
        var address = addressFakeGenerator.Generate();
        var name = nameFakeGenerator.Generate(typeName, address.City, address.Street);

        return new Domain.Venues.Venue(
            name: name,
            venueTypeId: new VenueTypeId(Guid.NewGuid()),
            address: address,
            size: sizeFakeGenerator.Generate(),
            rating: ratingFakeGenerator.Generate(),
            description: descriptionFakeGenerator.Generate(typeName));
    }
}
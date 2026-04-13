using AdVision.Application.Generators;
using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;

namespace AdVision.Infrastructure.Generators;

public class VenueFakeGenerator(
    IVenueNameFakeGenerator nameFakeGenerator,
    IVenueAddressFakeGenerator addressFakeGenerator,
    IVenueSizeFakeGenerator sizeFakeGenerator,
    IVenueRatingFakeGenerator ratingFakeGenerator,
    IVenueDescriptionFakeGenerator descriptionFakeGenerator) : IVenueFakeGenerator
{
    public Venue Generate(string typeName)
    {
        var address = addressFakeGenerator.Generate();
        var name = nameFakeGenerator.Generate(typeName, address.City, address.Street);

        return new Venue(
            name: name,
            venueTypeId: new VenueTypeId(Guid.NewGuid()),
            address: address,
            size: sizeFakeGenerator.Generate(),
            rating: ratingFakeGenerator.Generate(),
            description: descriptionFakeGenerator.Generate(typeName));
    }
}
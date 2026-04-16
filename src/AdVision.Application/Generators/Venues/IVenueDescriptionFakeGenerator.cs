using AdVision.Domain.Venues;

namespace AdVision.Application.Generators.Venues;

public interface IVenueDescriptionFakeGenerator
{
    VenueDescription Generate(string typeName);
}
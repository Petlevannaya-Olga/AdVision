using AdVision.Domain.Venues;

namespace AdVision.Application.Generators;

public interface IVenueDescriptionFakeGenerator
{
    VenueDescription Generate(string typeName);
}
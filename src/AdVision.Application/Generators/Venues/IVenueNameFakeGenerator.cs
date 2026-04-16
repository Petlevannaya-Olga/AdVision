using AdVision.Domain.Venues;

namespace AdVision.Application.Generators.Venues;

public interface IVenueNameFakeGenerator
{
    VenueName Generate(string type, string city, string street);
}
using AdVision.Domain.Venues;

namespace AdVision.Application.Generators;

public interface IVenueNameFakeGenerator
{
    VenueName Generate(string type, string city, string street);
}
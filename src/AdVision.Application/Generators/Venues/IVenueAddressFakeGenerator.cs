using AdVision.Domain.Venues;

namespace AdVision.Application.Generators.Venues;

public interface IVenueAddressFakeGenerator
{
    VenueAddress Generate();
}
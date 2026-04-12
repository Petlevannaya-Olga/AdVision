using AdVision.Domain.Venues;

namespace AdVision.Application.Generators;

public interface IVenueAddressFakeGenerator
{
    VenueAddress Generate();
}
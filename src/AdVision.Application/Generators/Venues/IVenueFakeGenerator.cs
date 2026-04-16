using AdVision.Domain.Venues;

namespace AdVision.Application.Generators.Venues;

public interface IVenueFakeGenerator
{
    Venue Generate(string typeName);
}
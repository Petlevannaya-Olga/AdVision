using AdVision.Domain.Venues;

namespace AdVision.Application.Generators;

public interface IVenueFakeGenerator
{
    Venue Generate(string typeName);
}
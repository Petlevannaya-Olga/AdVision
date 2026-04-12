namespace AdVision.Contracts;

public sealed record VenueDto(
    string Name,
    string Type,
    string Region,
    string District,
    string City,
    string Street,
    string House,
    double Latitude,
    double Longitude,
    double Width,
    double Height,
    double Rating,
    string Description);
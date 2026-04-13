namespace AdVision.Contracts;

public sealed record CreateVenueDto(
    string Name,
    VenueTypeDto Type,
    AddressDto Address,
    VenueSizeDto Size,
    double Rating,
    string Description);
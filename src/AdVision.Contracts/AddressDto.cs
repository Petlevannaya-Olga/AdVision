namespace AdVision.Contracts;

public record AddressDto(
    string Region,
    string District,
    string City,
    string Street,
    string House,
    double Latitude,
    double Longitude);
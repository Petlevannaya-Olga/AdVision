using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Venues.CreateVenueCommand;

public sealed record CreateVenueCommand(CreateVenueDto Dto) : IValidation;
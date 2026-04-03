using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.VenueTypes.CreateVenueTypeCommand;

public sealed record CreateVenueTypeCommand(CreateVenueTypeDto Dto) : IValidation;
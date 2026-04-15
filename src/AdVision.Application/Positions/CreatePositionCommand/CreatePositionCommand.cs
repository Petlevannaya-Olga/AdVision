using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Positions.CreatePositionCommand;

public sealed record CreatePositionCommand(CreatePositionDto Dto) : IValidation;
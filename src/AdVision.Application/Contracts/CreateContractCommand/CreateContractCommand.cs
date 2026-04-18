using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Contracts.CreateContractCommand;

public sealed record CreateContractCommand(CreateContractDto Dto) : IValidation;
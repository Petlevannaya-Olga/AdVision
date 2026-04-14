using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Tariffs.CreateTariffCommand;

public sealed record CreateTariffCommand(CreateTariffDto Dto) : IValidation;
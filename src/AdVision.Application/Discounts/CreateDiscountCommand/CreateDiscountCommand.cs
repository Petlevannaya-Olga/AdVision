using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Discounts.CreateDiscountCommand;

public sealed record CreateDiscountCommand(CreateDiscountDto Dto) : IValidation;
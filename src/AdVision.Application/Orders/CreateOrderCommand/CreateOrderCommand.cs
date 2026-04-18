using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Orders.CreateOrderCommand;

public sealed record CreateOrderCommand(CreateOrderDto Dto) : IValidation;
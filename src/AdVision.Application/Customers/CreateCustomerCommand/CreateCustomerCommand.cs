using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Customers.CreateCustomerCommand;

public sealed record CreateCustomerCommand(CreateCustomerDto Dto) : IValidation;
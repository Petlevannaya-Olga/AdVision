using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Application.Employees.CreateEmployeeCommand;

public sealed record CreateEmployeeCommand(CreateEmployeeDto Dto) : IValidation;
using AdVision.Domain.Employees;

namespace AdVision.Application.Generators.Employees;

public interface IEmployeeAddressFakeGenerator
{
    EmployeeAddress Generate();
}
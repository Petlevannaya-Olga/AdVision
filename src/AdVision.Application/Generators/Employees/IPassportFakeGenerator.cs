using AdVision.Domain.Employees;

namespace AdVision.Application.Generators.Employees;

public interface IPassportFakeGenerator
{
    Passport Generate();
}
using AdVision.Application.Generators.Employees;
using AdVision.Domain.Employees;
using Bogus;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators.Employees;

public class EmployeeAddressFakeGenerator(
    ILogger<EmployeeAddressFakeGenerator> logger) : IEmployeeAddressFakeGenerator
{
    private readonly Faker _faker = new("ru");

    public EmployeeAddress Generate()
    {
        while (true)
        {
            var value = _faker.Address.FullAddress();

            var result = EmployeeAddress.Create(value);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерирован невалидный адрес сотрудника: {Address}",
                value);
        }
    }
}
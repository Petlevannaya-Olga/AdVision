using AdVision.Application.Generators.Employees;
using AdVision.Domain;
using AdVision.Domain.Employees;
using Bogus;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators.Employees;

public class PhoneNumberFakeGenerator(
    ILogger<PhoneNumberFakeGenerator> logger) : IPhoneNumberFakeGenerator
{
    private readonly Faker _faker = new();

    public PhoneNumber Generate()
    {
        while (true)
        {
            var value = _faker.Phone.PhoneNumber("+7##########");

            var result = PhoneNumber.Create(value);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерирован невалидный номер телефона: {PhoneNumber}",
                value);
        }
    }
}
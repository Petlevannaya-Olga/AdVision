using AdVision.Application.Generators.Employees;
using AdVision.Domain.Employees;
using Bogus;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators.Employees;

public class PassportFakeGenerator(
    ILogger<PassportFakeGenerator> logger) : IPassportFakeGenerator
{
    private readonly Faker _faker = new();

    public Passport Generate()
    {
        while (true)
        {
            var series = _faker.Random.Replace("####");
            var number = _faker.Random.Replace("######");

            var result = Passport.Create(series, number);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерирован невалидный паспорт: Series={Series}, Number={Number}",
                series,
                number);
        }
    }
}
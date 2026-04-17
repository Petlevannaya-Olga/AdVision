using AdVision.Application.Generators.Employees;
using AdVision.Domain;
using AdVision.Domain.Employees;
using Bogus;
using Bogus.DataSets;
using Microsoft.Extensions.Logging;

namespace AdVision.Infrastructure.Generators.Employees;

public class PersonNameFakeGenerator(
    ILogger<PersonNameFakeGenerator> logger) : IPersonNameFakeGenerator
{
    private readonly Faker _faker = new("ru");

    public Name.Gender GenerateGender()
        => _faker.PickRandom<Name.Gender>();

    public PersonName GenerateFirstName(Name.Gender gender)
    {
        while (true)
        {
            var value = _faker.Name.FirstName(gender);

            var result = PersonName.Create(value);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерировано невалидное имя: {FirstName}",
                value);
        }
    }

    public PersonName GenerateLastName(Name.Gender gender)
    {
        while (true)
        {
            var value = _faker.Name.LastName(gender);

            var result = PersonName.Create(value);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерирована невалидная фамилия: {LastName}",
                value);
        }
    }

    public PersonName GenerateMiddleName(Name.Gender gender)
    {
        while (true)
        {
            var value = gender == Name.Gender.Female
                ? _faker.PickRandom(
                    "Александровна", "Алексеевна", "Андреевна",
                    "Владимировна", "Дмитриевна", "Ивановна")
                : _faker.PickRandom(
                    "Александрович", "Алексеевич", "Андреевич",
                    "Владимирович", "Дмитриевич", "Иванович");

            var result = PersonName.Create(value);

            if (result.IsSuccess)
                return result.Value;

            logger.LogDebug(
                "Сгенерировано невалидное отчество: {MiddleName}",
                value);
        }
    }
}
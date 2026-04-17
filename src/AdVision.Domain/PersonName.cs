using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain;

public sealed class PersonName
{
    public const int MIN_LENGTH = 2;
    public const int MAX_LENGTH = 100;

    public string Value { get; private set; }

    private PersonName(string value)
    {
        Value = value;
    }

    public static Result<PersonName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CommonErrors.IsRequired(nameof(value));
        }

        var trimmed = value.Trim();

        if (trimmed.Length is < MIN_LENGTH or > MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(value), MIN_LENGTH, MAX_LENGTH);
        }

        return new PersonName(trimmed);
    }
}
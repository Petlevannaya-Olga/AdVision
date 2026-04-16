using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Employees;

public sealed class PhoneNumber
{
    public const int MIN_LENGTH = 10;
    public const int MAX_LENGTH = 20;

    public string Value { get; private set; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CommonErrors.IsRequired(nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length is < MIN_LENGTH or > MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(value), MIN_LENGTH, MAX_LENGTH);
        }

        return new PhoneNumber(normalized);
    }
}
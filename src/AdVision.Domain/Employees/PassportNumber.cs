using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Employees;

public sealed class PassportNumber
{
    public const int LENGTH = 6;

    public string Value { get; private set; }

    private PassportNumber(string value)
    {
        Value = value;
    }

    public static Result<PassportNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CommonErrors.IsRequired(nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length != LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(value), LENGTH, LENGTH);
        }

        if (!normalized.All(char.IsDigit))
        {
            return CommonErrors.Validation(
                "passport.number.invalid",
                "Номер паспорта должен содержать только цифры");
        }

        return new PassportNumber(normalized);
    }
}
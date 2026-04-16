using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Employees;

public sealed class PassportSeries
{
    public const int LENGTH = 4;

    public string Value { get; private set; }

    private PassportSeries(string value)
    {
        Value = value;
    }

    public static Result<PassportSeries, Error> Create(string value)
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
                "passport.series.invalid",
                "Серия паспорта должна содержать только цифры");
        }

        return new PassportSeries(normalized);
    }
}
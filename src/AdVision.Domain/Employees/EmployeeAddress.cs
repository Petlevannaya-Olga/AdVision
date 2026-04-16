using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Employees;

public sealed class EmployeeAddress
{
    public const int MIN_LENGTH = 5;
    public const int MAX_LENGTH = 300;

    public string Value { get; private set; }

    private EmployeeAddress(string value)
    {
        Value = value;
    }

    public static Result<EmployeeAddress, Error> Create(string value)
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

        return new EmployeeAddress(trimmed);
    }
}
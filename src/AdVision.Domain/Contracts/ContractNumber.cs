using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Contracts;

public sealed class ContractNumber
{
    /// <summary>
    /// Минимальная длина строки
    /// </summary>
    public const int MIN_LENGTH = 1;

    /// <summary>
    /// Максимальная длина строки
    /// </summary>
    public const int MAX_LENGTH = 50;

    /// <summary>
    /// Номер договора
    /// </summary>
    public string Value { get; private set; }

    private ContractNumber(string value)
    {
        Value = value;
    }

    public static Result<ContractNumber, Error> Create(string value)
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

        return new ContractNumber(trimmed);
    }
}
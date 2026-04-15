using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Positions;

public sealed class PositionName
{
    /// <summary>
    /// Минимальная длина строки
    /// </summary>
    public const int MIN_LENGTH = 3;

    /// <summary>
    /// Максимальная длина строки
    /// </summary>
    public const int MAX_LENGTH = 100;

    /// <summary>
    /// Название позиции
    /// </summary>
    public string Value { get; private set; }

    private PositionName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Фабричный метод
    /// </summary>
    public static Result<PositionName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CommonErrors.IsRequired(nameof(value));
        }

        if (value.Length is < MIN_LENGTH or > MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(value), MIN_LENGTH, MAX_LENGTH);
        }

        return new PositionName(value.Trim());
    }
}
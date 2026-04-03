using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Venues;

public sealed class VenueName
{
    /// <summary>
    /// Минимальное значение длины строки
    /// </summary>
    private const int MIN_LENGTH = 10;

    /// <summary>
    /// Максимальное значение длины строки
    /// </summary>
    private const int MAX_LENGTH = 500;

    /// <summary>
    /// Название площадки
    /// </summary>
    public string Value { get; private set; } 
    
    private VenueName(string name)
    {
        Value = name;
    }

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="value">Название</param>
    /// <returns>Новое название позиции</returns>
    public static Result<VenueName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CommonErrors.IsRequired(nameof(value));
        }

        if (value.Length is < MIN_LENGTH or > MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(value), MIN_LENGTH, MAX_LENGTH);
        }

        return new VenueName(value.Trim());
    }
}
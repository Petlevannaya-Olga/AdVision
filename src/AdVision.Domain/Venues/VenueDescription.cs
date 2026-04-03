using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Venues;

public sealed class VenueDescription(string Value)
{
    /// <summary>
    /// Минимальное длина строки
    /// </summary>
    public const int MIN_LENGTH = 100;
    
    /// <summary>
    /// Максимальная длина строки
    /// </summary>
    public const int MAX_LENGTH = 2000;
    
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; private set; } = Value;

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="value">Значение</param>
    /// <returns>Новое описание площадки</returns>
    public static Result<VenueDescription, Error> Create(string value)
    {
        return value.Length switch
        {
            > MAX_LENGTH => CommonErrors.LengthIsTooLarge(nameof(value), MAX_LENGTH),
            < MIN_LENGTH => CommonErrors.LengthIsTooShort(nameof(value), MIN_LENGTH),
            _ => new VenueDescription(value)
        };
    }
}
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.VenueTypes;

public sealed class VenueName(string name)
{
    /// <summary>
    /// Минимальное значение длины строки
    /// </summary>
    private const int MIN_LENGTH = 3;

    /// <summary>
    /// Максимальное значение длины строки
    /// </summary>
    private const int MAX_LENGTH = 100;

    /// <summary>
    /// Название площадки
    /// </summary>
    public string Name { get; private set; } = name;
    
    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="name">Название</param>
    /// <returns>Новое название позиции</returns>
    public static Result<VenueName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return CommonErrors.IsRequired(nameof(name));
        }

        if (name.Length is < MIN_LENGTH or > MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(name), MIN_LENGTH, MAX_LENGTH);
        }

        return new VenueName(name.Trim());
    }
}
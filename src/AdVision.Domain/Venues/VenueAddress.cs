using System.Diagnostics.CodeAnalysis;
using AdVision.Contracts;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Venues;

public sealed class VenueAddress(
    string Region,
    string District,
    string City,
    string Street,
    string House,
    double Latitude,
    double Longitude)
{
    /// <summary>
    /// Минимальное значение ширины
    /// </summary>
    public const int MIN_LATITUDE_VALUE = -90;

    /// <summary>
    /// Максимальное значение ширины
    /// </summary>
    public const int MAX_LATITUDE_VALUE = 90;

    /// <summary>
    /// Минимальное значение долготы
    /// </summary>
    public const int MIN_LONGITUDE_VALUE = -180;

    /// <summary>
    /// Максимальное значение долготы
    /// </summary>
    public const int MAX_LONGITUDE_VALUE = 180;
    
    /// <summary>
    /// Минимальная длина строки
    /// </summary>
    public const int MIN_LENGTH = 10;
    
    /// <summary>
    /// Максимальная длина строки
    /// </summary>
    public const int MAX_LENGTH = 300;

    /// <summary>
    /// Регион
    /// </summary>
    public string Region { get; private set; } = Region;

    /// <summary>
    /// Район
    /// </summary>
    public string District { get; private set; } = District;

    /// <summary>
    /// Город
    /// </summary>
    public string City { get; private set; } = City;

    /// <summary>
    /// Улица
    /// </summary>
    public string Street { get; private set; } = Street;

    /// <summary>
    /// Номер дома
    /// </summary>
    public string House { get; private set; } = House;

    /// <summary>
    /// Широта
    /// </summary>
    public double Latitude { get; private set; } = Latitude;

    /// <summary>
    /// Долгота
    /// </summary>
    public double Longitude { get; private set; } = Longitude;

    /// <summary>
    /// Фабричный метод
    /// </summary>
    /// <param name="dto">Address dto</param>
    /// <returns>Новый адрес</returns>
    public static Result<VenueAddress, Error> Create(AddressDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Region))
        {
            return CommonErrors.IsRequired(nameof(dto.Region));
        }

        if (string.IsNullOrWhiteSpace(dto.District))
        {
            return CommonErrors.IsRequired(nameof(dto.District));
        }

        if (string.IsNullOrWhiteSpace(dto.City))
        {
            return CommonErrors.IsRequired(nameof(dto.City));
        }

        if (string.IsNullOrWhiteSpace(dto.Street))
        {
            return CommonErrors.IsRequired(nameof(dto.Street));
        }

        if (string.IsNullOrWhiteSpace(dto.House))
        {
            return CommonErrors.IsRequired(nameof(dto.House));
        }

        if (dto.Region.Length is <= MIN_LENGTH or MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(dto.Region), MIN_LENGTH, MAX_LENGTH);
        }
        
        if (dto.District.Length is <= MIN_LENGTH or MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(dto.District), MIN_LENGTH, MAX_LENGTH);
        }
        
        if (dto.City.Length is <= MIN_LENGTH or MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(dto.City), MIN_LENGTH, MAX_LENGTH);
        }

        if (dto.Street.Length is <= MIN_LENGTH or MAX_LENGTH)
        {
            return CommonErrors.LengthIsWrong(nameof(dto.Street), MIN_LENGTH, MAX_LENGTH);
        }
        
        if (dto.House.Length < 1)
        {
            return CommonErrors.LengthIsTooShort(nameof(dto.House), 1);
        }
        
        if (dto.Latitude is <= -90 or >= 90)
        {
            return AddressErrors.WrongInterval(
                value: dto.Latitude,
                min: MIN_LATITUDE_VALUE,
                max: MAX_LATITUDE_VALUE,
                invalidField: nameof(dto.Latitude));
        }

        if (dto.Longitude is <= -180 or >= 180)
        {
            return AddressErrors.WrongInterval(
                value: dto.Longitude,
                min: MIN_LONGITUDE_VALUE,
                max: MAX_LONGITUDE_VALUE,
                invalidField: nameof(dto.Longitude));
        }

        return new VenueAddress(
            dto.Region,
            dto.District,
            dto.City,
            dto.Street,
            dto.House,
            dto.Latitude,
            dto.Longitude);
    }

    /// <summary>
    /// Ошибки, которые может возвращать сущность
    /// </summary>
    [ExcludeFromCodeCoverage]
    private static class AddressErrors
    {
        public static Error WrongNumber(int number, string invalidField)
        {
            return new Error(
                $"{number}.is.wrong.number",
                $"Номер должен быть больше нуля:  {number}",
                ErrorType.VALIDATION,
                invalidField);
        }

        public static Error WrongInterval(double value, int min, int max, string invalidField)
        {
            return new Error(
                $"{value}.is.wrong.interval",
                $"Значение должно быть в диапазоне от {min} до {max}",
                ErrorType.VALIDATION,
                invalidField);
        }
    }
}
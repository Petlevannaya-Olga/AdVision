using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Venues;

public sealed class VenueSize(double Width, double Height)
{
    /// <summary>
    /// Максимальная высота
    /// </summary>
    public const double MAX_HEIGHT = 10_000;

    /// <summary>
    /// Максимальная ширина
    /// </summary>
    public const double MAX_WIDTH = 10_000;
    
    /// <summary>
    /// Минимальная ширина
    /// </summary>
    public const double MIN_WIDTH = 100;
    
    /// <summary>
    /// Минимальная высота
    /// </summary>
    public const double MIN_HEIGHT = 100;

    /// <summary>
    /// Ширина
    /// </summary>
    public double Width { get; private set; } = Width;

    /// <summary>
    /// Высота
    /// </summary>
    public double Height { get; private set; } = Height;

    public static Result<VenueSize, Error> Create(double width, double height)
    {
        if (width < 0)
        {
            return CommonErrors.MustBePositive(nameof(width));
        }

        if (height < 0)
        {
            return CommonErrors.MustBePositive(nameof(height));
        }

        return width switch
        {
            > MAX_WIDTH => CommonErrors.ValueIsGreaterThanMax(nameof(width), width, MAX_WIDTH),
            < MIN_WIDTH => CommonErrors.ValueIsLessThanMin(nameof(width), width, MIN_WIDTH),
            _ => height switch
            {
                > MAX_HEIGHT => CommonErrors.ValueIsGreaterThanMax(nameof(height), height, MAX_HEIGHT),
                < MIN_HEIGHT => CommonErrors.ValueIsLessThanMin(nameof(height), width, MIN_HEIGHT),
                _ => new VenueSize(width, height)
            }
        };
    }
}
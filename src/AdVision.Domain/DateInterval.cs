using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain;

public class DateInterval
{
    /// <summary>
    /// Дата начала
    /// </summary>
    public DateOnly StartDate { get; private set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    public DateOnly EndDate { get; private set; }

    /// <summary>
    /// Приватный конструктор
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окончания</param>
    private DateInterval(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Result<DateInterval, Error> Create(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
        {
            return DateIntervalErrors.WrongInterval(startDate, endDate);
        }

        return new DateInterval(startDate, endDate);
    }

    [ExcludeFromCodeCoverage]
    private static class DateIntervalErrors
    {
        public static Error WrongInterval(DateOnly startDate, DateOnly endDate)
        {
            return new Error(
                $"from.{startDate}.to.{endDate}.is.wrong.interval",
                $"Дата начала не может быть больше даты окончания",
                ErrorType.VALIDATION);
        }
    }
}
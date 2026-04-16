using CSharpFunctionalExtensions;
using Shared;

namespace AdVision.Domain.Employees;

public sealed class Passport
{
    public PassportSeries Series { get; private set; }
    public PassportNumber Number { get; private set; }

    private Passport(PassportSeries series, PassportNumber number)
    {
        Series = series;
        Number = number;
    }

    public static Result<Passport, Error> Create(
        PassportSeries series,
        PassportNumber number)
    {
        return new Passport(series, number);
    }

    public static Result<Passport, Error> Create(
        string series,
        string number)
    {
        var seriesResult = PassportSeries.Create(series);
        if (seriesResult.IsFailure)
        {
            return seriesResult.Error;
        }

        var numberResult = PassportNumber.Create(number);
        if (numberResult.IsFailure)
        {
            return numberResult.Error;
        }

        return new Passport(seriesResult.Value, numberResult.Value);
    }
}
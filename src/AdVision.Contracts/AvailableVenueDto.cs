namespace AdVision.Contracts;

public sealed record AvailableVenueDto(
    Guid VenueId,
    Guid TariffId,
    string VenueName,
    string VenueTypeName,
    string Region,
    string District,
    string City,
    string Street,
    int Rating,
    decimal Price,
    DateOnly TariffStartDate,
    DateOnly TariffEndDate,
    int FreeDaysCount,
    IReadOnlyList<DateOnly> BusyDates,
    bool HasPartialAvailability)
{
    public string BusyDatesText => FormatDateRanges(BusyDates);
    
    private static string FormatDateRanges(IEnumerable<DateOnly> dates)
    {
        var ordered = dates
            .Distinct()
            .OrderBy(d => d)
            .ToList();

        if (ordered.Count == 0)
        {
            return "—";
        }

        var ranges = new List<string>();

        var start = ordered[0];
        var prev = ordered[0];

        for (int i = 1; i < ordered.Count; i++)
        {
            var current = ordered[i];

            if (current.DayNumber == prev.DayNumber + 1)
            {
                prev = current;
                continue;
            }

            ranges.Add(FormatRange(start, prev));

            start = current;
            prev = current;
        }

        ranges.Add(FormatRange(start, prev));

        return string.Join(", ", ranges);
    }

    private static string FormatRange(DateOnly start, DateOnly end)
    {
        if (start == end)
        {
            return start.ToString("dd.MM");
        }

        return $"{start:dd.MM}–{end:dd.MM}";
    }
}
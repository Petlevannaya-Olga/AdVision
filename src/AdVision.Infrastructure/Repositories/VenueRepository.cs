using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Orders;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure.Repositories;

public class VenueRepository(ApplicationDbContext dbContext, ILogger<VenueRepository> logger) : IVenueRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Venue venue, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Venues.AddAsync(venue, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteException)
        {
            // SQLITE_CONSTRAINT = 19
            if (sqliteException.SqliteErrorCode == 19 &&
                sqliteException.Message.Contains("venues", StringComparison.InvariantCultureIgnoreCase))
            {
                return CommonErrors.Conflict("venue.is.conflict", "Площадка уже существует");
            }

            logger.LogError(e, "Ошибка добавления новой площадки '{VenueName}'", venue.Name.Value);

            return CommonErrors.Db(
                "add.venue.to.db.exception",
                $"Ошибка добавления новой площадки '{venue.Name.Value}'");
        }
        catch (OperationCanceledException e)
        {
            logger.LogError(e, "Операция создания новой площадки '{VenueName}' была отменена", venue.Name.Value);

            return CommonErrors.OperationCancelled("add.venue.was.canceled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка добавления новой площадки '{VenueName}'", venue.Name.Value);

            return CommonErrors.Db(
                "add.venue.to.db.exception",
                $"Ошибка добавления новой площадки '{venue.Name.Value}'");
        }

        return venue.Id.Value;
    }

    public async Task<Result<Venue?, Error>> GetByAsync(Expression<Func<Venue, bool>> expression,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Venues
                .FirstOrDefaultAsync(expression, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения площадки была отменена");
            return CommonErrors.OperationCancelled("get.venue.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении площадки");
            return CommonErrors.Db(
                "get.venue.from.db.exception",
                "Ошибка при получении площадки");
        }
    }

    public async Task<Result<PagedResult<Venue>, Error>> GetAsync(
        int page,
        int size,
        Expression<Func<Venue, bool>>? filter = null,
        Expression<Func<Venue, object>>? orderBy = null,
        bool orderByDescending = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = dbContext.Venues
                .Include(v => v.Type)
                .AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            query = ApplySorting(query, orderBy, orderByDescending);

            var totalCount = await query.CountAsync(cancellationToken);

            var venues = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return new PagedResult<Venue>(venues, page, size, totalCount);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Операция получения площадок была отменена");
            return CommonErrors.OperationCancelled("get.venues.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении площадок");
            return CommonErrors.Db(
                "get.venues.from.db.exception",
                "Ошибка при получении площадок");
        }
    }

    public async Task<Result<IReadOnlyList<string>, Error>> GetDistinctAsync(
        Expression<Func<Venue, string>> selector,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await dbContext
                .Venues
                .AsNoTracking()
                .Select(selector)
                .Distinct()
                .ToListAsync(cancellationToken);

            return result;
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения значений была отменена");
            return CommonErrors.OperationCancelled("get.values.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении значений");
            return CommonErrors.Db(
                "get.values.from.db.exception",
                "Ошибка при получении значений");
        }
    }

    private static IQueryable<Venue> ApplySorting(
        IQueryable<Venue> query,
        Expression<Func<Venue, object>>? orderBy,
        bool orderByDescending)
    {
        if (orderBy is null)
        {
            return query.OrderBy(v => v.Name.Value);
        }

        return orderByDescending
            ? query.OrderByDescending(orderBy)
            : query.OrderBy(orderBy);
    }

    public async Task<Result<PagedResult<AvailableVenueDto>, Error>> GetAvailableAsync(
        int page,
        int pageSize,
        string? name,
        Guid? venueTypeId,
        string? region,
        string? district,
        string? city,
        string? street,
        int ratingFrom,
        int ratingTo,
        decimal? priceFrom,
        decimal? priceTo,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        CancellationToken cancellationToken)
    {
        try
        {
            if (page <= 0)
            {
                page = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 20;
            }

            var venues = await dbContext.Venues
                .Include(x => x.Type)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var tariffs = await dbContext.Tariffs
                .Include(x => x.Venue)
                .ThenInclude(x => x.Type)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var orderItems = await dbContext.OrderItems
                .Include(x => x.Order)
                .Include(x => x.Tariff)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var filteredVenues = venues
                .Where(v =>
                    (string.IsNullOrWhiteSpace(name) ||
                     v.Name.Value.Contains(name.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                    (!venueTypeId.HasValue || v.VenueTypeId.Value == venueTypeId.Value) &&
                    (string.IsNullOrWhiteSpace(region) ||
                     string.Equals(v.Address.Region, region.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(district) ||
                     string.Equals(v.Address.District, district.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(city) ||
                     string.Equals(v.Address.City, city.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(street) ||
                     v.Address.Street.Contains(street.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                    v.Rating.Value >= ratingFrom &&
                    v.Rating.Value <= ratingTo)
                .ToList();

            var venueIds = filteredVenues
                .Select(x => x.Id)
                .ToHashSet();

            var filteredTariffs = tariffs
                .Where(t => venueIds.Contains(t.VenueId))
                .ToList();

            if (priceFrom.HasValue)
            {
                filteredTariffs = filteredTariffs
                    .Where(t => t.Price.Value >= priceFrom.Value)
                    .ToList();
            }

            if (priceTo.HasValue)
            {
                filteredTariffs = filteredTariffs
                    .Where(t => t.Price.Value <= priceTo.Value)
                    .ToList();
            }

            var items = new List<AvailableVenueDto>();

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                if (dateFrom.Value > dateTo.Value)
                {
                    return CommonErrors.Validation(
                        "position.date.interval.is.invalid",
                        "Дата начала не может быть больше даты окончания");
                }

                filteredTariffs = filteredTariffs
                    .Where(t =>
                        t.Interval.StartDate <= dateTo.Value &&
                        t.Interval.EndDate >= dateFrom.Value)
                    .ToList();

                foreach (var tariff in filteredTariffs)
                {
                    var requestedDays = Enumerable
                        .Range(0, dateTo.Value.DayNumber - dateFrom.Value.DayNumber + 1)
                        .Select(offset => dateFrom.Value.AddDays(offset))
                        .ToList();

                    var tariffDays = requestedDays
                        .Where(day =>
                            day >= tariff.Interval.StartDate &&
                            day <= tariff.Interval.EndDate)
                        .ToHashSet();

                    if (tariffDays.Count == 0)
                    {
                        continue;
                    }

                    var busyDays = orderItems
                        .Where(oi =>
                            oi.Order.Status != OrderStatus.Cancelled &&
                            oi.Status != OrderItemStatus.Cancelled &&
                            oi.Tariff.VenueId == tariff.VenueId &&
                            oi.Period.StartDate <= dateTo.Value &&
                            oi.Period.EndDate >= dateFrom.Value)
                        .SelectMany(oi =>
                        {
                            var start = oi.Period.StartDate > dateFrom.Value
                                ? oi.Period.StartDate
                                : dateFrom.Value;

                            var end = oi.Period.EndDate < dateTo.Value
                                ? oi.Period.EndDate
                                : dateTo.Value;

                            return Enumerable
                                .Range(0, end.DayNumber - start.DayNumber + 1)
                                .Select(offset => start.AddDays(offset));
                        })
                        .ToHashSet();

                    var freeDaysCount = tariffDays.Count(day => !busyDays.Contains(day));

                    if (freeDaysCount <= 0)
                    {
                        continue;
                    }

                    items.Add(new AvailableVenueDto(
                        tariff.VenueId.Value,
                        tariff.Id.Value,
                        tariff.Venue.Name.Value,
                        tariff.Venue.Type.Name.Value,
                        tariff.Venue.Address.Region,
                        tariff.Venue.Address.District,
                        tariff.Venue.Address.City,
                        tariff.Venue.Address.Street,
                        (int)tariff.Venue.Rating.Value,
                        tariff.Price.Value,
                        tariff.Interval.StartDate,
                        tariff.Interval.EndDate,
                        freeDaysCount,
                        freeDaysCount < tariffDays.Count));
                }
            }
            else
            {
                items = filteredTariffs
                    .Select(t => new AvailableVenueDto(
                        t.VenueId.Value,
                        t.Id.Value,
                        t.Venue.Name.Value,
                        t.Venue.Type.Name.Value,
                        t.Venue.Address.Region,
                        t.Venue.Address.District,
                        t.Venue.Address.City,
                        t.Venue.Address.Street,
                        (int)t.Venue.Rating.Value,
                        t.Price.Value,
                        t.Interval.StartDate,
                        t.Interval.EndDate,
                        0,
                        false))
                    .ToList();
            }

            items = items
                .OrderBy(x => x.VenueName)
                .ThenBy(x => x.Price)
                .ThenByDescending(x => x.FreeDaysCount)
                .ToList();

            var totalCount = items.Count;

            var pagedItems = items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Result.Success<PagedResult<AvailableVenueDto>, Error>(
                new PagedResult<AvailableVenueDto>(
                    pagedItems,
                    page,
                    pageSize,
                    totalCount));
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения доступных площадок для позиции была отменена");
            return CommonErrors.OperationCancelled("get.available.venues.for.position.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения доступных площадок для позиции");
            return CommonErrors.Db(
                "get.available.venues.for.position.from.db.exception",
                "Ошибка получения доступных площадок для позиции");
        }
    }

    public async Task<Result<bool, Error>> IsAvailableForBookingAsync(
        VenueId venueId,
        DateOnly dateFrom,
        DateOnly dateTo,
        CancellationToken cancellationToken)
    {
        try
        {
            if (dateFrom > dateTo)
            {
                return CommonErrors.Validation(
                    "venue.booking.date.interval.is.invalid",
                    "Дата начала не может быть больше даты окончания");
            }

            var tariffs = await dbContext.Tariffs
                .Where(x => x.VenueId == venueId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (tariffs.Count == 0)
            {
                return Result.Success<bool, Error>(false);
            }

            var coveringTariffs = tariffs
                .Where(t =>
                    t.Interval.StartDate <= dateFrom &&
                    t.Interval.EndDate >= dateTo)
                .ToList();

            if (coveringTariffs.Count == 0)
            {
                return Result.Success<bool, Error>(false);
            }

            var tariffIds = coveringTariffs
                .Select(x => x.Id)
                .ToHashSet();

            var orderItems = await dbContext.OrderItems
                .Include(x => x.Order)
                .Include(x => x.Tariff)
                .Where(x =>
                    tariffIds.Contains(x.TariffId) &&
                    x.Order.Status != OrderStatus.Cancelled &&
                    x.Status != OrderItemStatus.Cancelled &&
                    x.Period.StartDate <= dateTo &&
                    x.Period.EndDate >= dateFrom)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var requestedDays = Enumerable
                .Range(0, dateTo.DayNumber - dateFrom.DayNumber + 1)
                .Select(dateFrom.AddDays)
                .ToHashSet();

            foreach (var tariff in coveringTariffs)
            {
                var busyDays = orderItems
                    .Where(oi => oi.TariffId == tariff.Id)
                    .SelectMany(oi =>
                    {
                        var start = oi.Period.StartDate > dateFrom
                            ? oi.Period.StartDate
                            : dateFrom;

                        var end = oi.Period.EndDate < dateTo
                            ? oi.Period.EndDate
                            : dateTo;

                        return Enumerable
                            .Range(0, end.DayNumber - start.DayNumber + 1)
                            .Select(start.AddDays);
                    })
                    .ToHashSet();

                var allDaysAreFree = requestedDays.All(day => !busyDays.Contains(day));

                if (allDaysAreFree)
                {
                    return Result.Success<bool, Error>(true);
                }
            }

            return Result.Success<bool, Error>(false);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция проверки доступности площадки для бронирования была отменена");
            return CommonErrors.OperationCancelled("venue.is.available.for.booking.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка проверки доступности площадки для бронирования");
            return CommonErrors.Db(
                "venue.is.available.for.booking.from.db.exception",
                "Ошибка проверки доступности площадки для бронирования");
        }
    }
}
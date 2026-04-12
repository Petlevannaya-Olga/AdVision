using System.Linq.Expressions;
using AdVision.Application;
using AdVision.Domain.Venues;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure;

public class VenueRepository(ApplicationDbContext dbContext, ILogger<VenueRepository> logger) : IVenueRepository
{
    public async Task<Result<Guid, Error>> AddAsync(Venue venue, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Venue?, Error>> GetByAsync(Expression<Func<Venue, bool>> expression,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IReadOnlyList<Venue>, Error>> GetAsync(int page, int size,
        CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext
                .Venues
                .Include(v => v.Type)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения всех типов площадок была отменена");
            return CommonErrors.OperationCancelled("get.venue.types.operation.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех площадок");
            return CommonErrors.Db(
                "get.venue.types.from.db.exception",
                "Ошибка при получении всех площадок");
        }
    }
}
using AdVision.Application.Repositories;
using AdVision.Contracts;
using AdVision.Domain.Orders;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace AdVision.Infrastructure.Repositories;

public sealed class OrderItemRepository(
    ApplicationDbContext dbContext,
    ILogger<OrderItemRepository> logger)
    : IOrderItemRepository
{
    public async Task<Result<IReadOnlyList<OrderItemDto>, Error>> GetByOrderIdAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        try
        {
            var items = await dbContext.OrderItems
                .Include(x => x.Tariff)
                .ThenInclude(x => x.Venue)
                .Where(x => x.OrderId == new OrderId(orderId))
                .AsNoTracking()
                .OrderBy(x => x.Period.StartDate)
                .ThenBy(x => x.Period.EndDate)
                .Select(x => new OrderItemDto(
                    x.Id.Value,
                    x.TariffId.Value,
                    x.Tariff.VenueId.Value,
                    x.Tariff.Venue.Name.Value,
                    x.Tariff.Price.Value,
                    x.Period.StartDate,
                    x.Period.EndDate,
                    x.Status.ToString()))
                .ToListAsync(cancellationToken);

            return Result.Success<IReadOnlyList<OrderItemDto>, Error>(items);
        }
        catch (OperationCanceledException)
        {
            logger.LogError("Операция получения позиций заказа была отменена");
            return CommonErrors.OperationCancelled("get.order.items.was.canceled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения позиций заказа");
            return CommonErrors.Db(
                "get.order.items.from.db.exception",
                "Ошибка получения позиций заказа");
        }
    }
}
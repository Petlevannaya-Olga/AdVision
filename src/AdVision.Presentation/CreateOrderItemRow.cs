namespace AdVision.Presentation;

public sealed record CreateOrderItemRow(
    Guid TariffId,
    string TariffName,
    DateOnly StartDate,
    DateOnly EndDate);
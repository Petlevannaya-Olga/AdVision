namespace AdVision.Domain.Orders;

public enum OrderItemStatus
{
    Planned = 0,     // Запланировано (ещё не началось)
    InProgress = 1,  // В работе
    Completed = 2,   // Завершено
    Cancelled = 3    // Отменено
}
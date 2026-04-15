namespace AdVision.Domain.Positions;

public sealed class Position
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public PositionId Id { get; private set; }

    /// <summary>
    /// Название
    /// </summary>
    public PositionName Name { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Название должности</param>
    public Position(PositionName name)
    {
        Id = new PositionId(Guid.NewGuid());
        Name = name;
    }

    // EF Core
    private Position()
    {
    }
}
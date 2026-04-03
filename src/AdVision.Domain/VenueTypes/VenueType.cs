namespace AdVision.Domain.VenueTypes;

public sealed class VenueType
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public VenueTypeId Id { get; private set; } = new(Guid.NewGuid());
}
using AdVision.Domain.Venues;

namespace AdVision.Domain.VenueTypes;

public sealed class VenueType
{
    /// <summary>
    /// Идентификатор, PK
    /// </summary>
    public VenueTypeId Id { get; private set; }

    /// <summary>
    /// Название
    /// </summary>
    public VenueTypeName Name { get; private set; }

    public VenueType(VenueTypeName name)
    {
        Id = new VenueTypeId(Guid.NewGuid());
        Name = name;
    }

    // EF Core
    private VenueType()
    {
    }
}
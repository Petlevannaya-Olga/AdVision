using Shared;

namespace AdVision.Application.SharedErrors;

public static class VenueTypeErrors
{
    public static Error VenueTypeNameConflict(string name)
    {
        return CommonErrors.Conflict(
            "venue.type.name.conflict",
            $"Тип площадки {name} уже существует");
    }
}
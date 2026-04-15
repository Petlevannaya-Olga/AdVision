using System.Diagnostics.CodeAnalysis;
using Shared;

namespace AdVision.Application.SharedErrors;

[ExcludeFromCodeCoverage]
public static class PositionErrors
{
    public static Error PositionNameConflict(string name) => CommonErrors.Conflict(
        "position.name.conflict",
        $"Позиция с названием '{name}' уже существует");
}
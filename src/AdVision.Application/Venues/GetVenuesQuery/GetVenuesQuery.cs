using Shared.Abstractions;

namespace AdVision.Application.Venues.GetVenuesQuery;

public sealed record GetVenuesQuery(int Page, int Size) : IQuery;
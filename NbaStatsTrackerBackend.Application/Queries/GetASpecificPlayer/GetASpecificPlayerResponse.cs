using NbaStatsTrackerBackend.Domain.Entities;

namespace NbaStatsTrackerBackend.Application.Queries.GetASpecificPlayer
{
    public record GetASpecificPlayerResponse(IReadOnlyList<NbaPlayers> Players);
}

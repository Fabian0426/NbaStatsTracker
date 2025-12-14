using NbaStatsTrackerBackend.Domain.Entities;

namespace NbaStatsTrackerBackend.Application.Queries.GetAllPlayers;

public record GetAllPlayersResponse(IReadOnlyList<NbaPlayers> Players);
using NbaStatsTrackerBackend.Domain.Entities;
namespace NbaStatsTrackerBackend.Application.Queries.GetASpecificGame;
public record GetASpecificGameResponse(IReadOnlyList<NbaGames> Games);
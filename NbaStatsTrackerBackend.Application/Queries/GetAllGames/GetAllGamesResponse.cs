using NbaStatsTrackerBackend.Domain.Entities;
namespace NbaStatsTrackerBackend.Application.Queries.GetAllGames;

public record GetAllGamesResponse(IReadOnlyList<NbaGames> Games);
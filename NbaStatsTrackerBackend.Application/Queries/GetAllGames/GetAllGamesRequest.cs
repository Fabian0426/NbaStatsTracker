using MediatR;
namespace NbaStatsTrackerBackend.Application.Queries.GetAllGames;

public record GetAllGamesRequest(
    int? cursor,
    int? per_page,
    List<DateTime>? dates,
    List<int>? seasons,
    List<int>? team_ids,
    Boolean? postseason,
    DateTime? start_date,
    DateTime? end_date
) : IRequest<GetAllGamesResponse>;
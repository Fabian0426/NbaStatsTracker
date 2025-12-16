using MediatR;

namespace NbaStatsTrackerBackend.Application.Queries.GetAllPlayers;

public record GetAllPlayersRequest(
    int? cursor,
    int? per_page,
    string? search,
    string? first_name,
    string? last_name,
    List<int>? team_ids,
    List<int>? player_ids
) : IRequest<GetAllPlayersResponse>;
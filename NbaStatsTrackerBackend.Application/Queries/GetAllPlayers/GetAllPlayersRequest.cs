using MediatR;

namespace NbaStatsTrackerBackend.Application.Queries.GetAllPlayers;

public record GetAllPlayersRequest(
    string? Search,
    int? Page,
    int? PerPage
) : IRequest<GetAllPlayersResponse>;
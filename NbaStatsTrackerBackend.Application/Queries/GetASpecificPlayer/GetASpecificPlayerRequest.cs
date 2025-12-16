using MediatR;

namespace NbaStatsTrackerBackend.Application.Queries.GetASpecificPlayer;

public record GetASpecificPlayerRequest(
    int Id
) : IRequest<GetASpecificPlayerResponse>;
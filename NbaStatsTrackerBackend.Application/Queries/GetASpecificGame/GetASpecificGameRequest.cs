using MediatR;
namespace NbaStatsTrackerBackend.Application.Queries.GetASpecificGame;

public record GetASpecificGameRequest(
    int Id
) : IRequest<GetASpecificGameResponse>;
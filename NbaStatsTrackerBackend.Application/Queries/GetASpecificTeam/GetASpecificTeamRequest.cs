using MediatR;
namespace NbaStatsTrackerBackend.Application.UseCases.GetASpecificTeam;

public record GetASpecificTeamRequest(
    int Id
) : IRequest<GetASpecificTeamResponse>;
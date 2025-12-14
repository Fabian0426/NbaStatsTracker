using MediatR;
namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;

public record GetAllTeamsRequest(
    string? conference,
    string? division
) : IRequest<GetAllTeamsResponse>;
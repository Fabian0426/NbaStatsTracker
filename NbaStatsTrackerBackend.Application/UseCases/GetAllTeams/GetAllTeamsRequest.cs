using MediatR;

namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;

public record GetAllTeamsRequest(
    string? conference = null,
    string? division = null
) : IRequest<GetAllTeamsResponse>;
using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.UseCases.GetASpecificTeam;

public class GetASpecificTeamHandler : IRequestHandler<GetASpecificTeamRequest, GetASpecificTeamResponse>
{
    private readonly IBalldontlieApiClient _apiClient;

    public GetASpecificTeamHandler(IBalldontlieApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetASpecificTeamResponse> Handle(
        GetASpecificTeamRequest request, 
        CancellationToken cancellationToken)
    {
        string endpoint = $"v1/teams/{request.Id}";

        var jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, cancellationToken);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var teamElement) != true)
            return new GetASpecificTeamResponse([]);

        
        var team = new NbaTeams(
            teamElement.GetProperty("id").GetInt32(),
            teamElement.GetProperty("conference").GetString() is string c && !string.IsNullOrWhiteSpace(c) ? c : " -- ",
            teamElement.GetProperty("division").GetString() is string d && !string.IsNullOrWhiteSpace(d) ? d : "no division avalaible",
            teamElement.GetProperty("city").GetString() is string city && !string.IsNullOrWhiteSpace(city) ? city : "team no longer has a city because it not exists",
            teamElement.GetProperty("name").GetString() ?? string.Empty,
            teamElement.GetProperty("full_name").GetString() ?? string.Empty,
            teamElement.GetProperty("abbreviation").GetString() ?? string.Empty);

        return new GetASpecificTeamResponse([team]);
    }
}

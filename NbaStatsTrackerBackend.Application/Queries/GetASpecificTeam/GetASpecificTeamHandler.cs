using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using NbaStatsTrackerBackend.Application.Utils;
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
            Utils.Utils.GetStringSafe(teamElement, "conference") is string c && !string.IsNullOrWhiteSpace(c) ? c : " -- ",
            Utils.Utils.GetStringSafe(teamElement, "division") is string d && !string.IsNullOrWhiteSpace(d) ? d : "no division available",
            Utils.Utils.GetStringSafe(teamElement, "city") is string city && !string.IsNullOrWhiteSpace(city) ? city : "team no longer has a city because it not exists",
            Utils.Utils.GetStringSafe(teamElement, "name"),
            Utils.Utils.GetStringSafe(teamElement, "full_name"),
            Utils.Utils.GetStringSafe(teamElement, "abbreviation")
        );

        return new GetASpecificTeamResponse([team]);
    }
}

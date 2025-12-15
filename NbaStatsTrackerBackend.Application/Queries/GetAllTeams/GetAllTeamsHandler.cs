using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;

public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsRequest, GetAllTeamsResponse>
{
    private readonly IBalldontlieApiClient _apiClient;

    public GetAllTeamsHandler(IBalldontlieApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetAllTeamsResponse> Handle(
        GetAllTeamsRequest request,
        CancellationToken cancellationToken)
    {
        List<string> queryParams = new List<string>();

        string endpoint = "v1/teams";
        if (queryParams.Any())
            endpoint += "?" + string.Join("&", queryParams);

        var jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, cancellationToken);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var data) != true)
            return new GetAllTeamsResponse([]);

        List<NbaTeams> teams = new List<NbaTeams>();
        foreach (var teamElement in data.EnumerateArray())
        {
            teams.Add(new NbaTeams(
                teamElement.GetProperty("id").GetInt32(),
                teamElement.GetProperty("conference").GetString() is string c && !string.IsNullOrWhiteSpace(c) ? c : " -- ",
                teamElement.GetProperty("division").GetString() is string d && !string.IsNullOrWhiteSpace(d) ? d : "no division avalaible",
                teamElement.GetProperty("city").GetString() is string city && !string.IsNullOrWhiteSpace(city) ? city : "team no longer has a city because it not exists",
                teamElement.GetProperty("name").GetString() ?? string.Empty,
                teamElement.GetProperty("full_name").GetString() ?? string.Empty,
                teamElement.GetProperty("abbreviation").GetString() ?? string.Empty));
        }

        return new GetAllTeamsResponse(teams);
    }
}
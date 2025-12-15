using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.Queries.GetAllPlayers;

public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersRequest, GetAllPlayersResponse>
{
    private readonly IBalldontlieApiClient _apiClient;

    public GetAllPlayersHandler(IBalldontlieApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetAllPlayersResponse> Handle(
        GetAllPlayersRequest request,
        CancellationToken cancellationToken)
    {
        List<string> queryParams = new List<string>();

        if (!string.IsNullOrWhiteSpace(request.Search))
            queryParams.Add($"search={request.Search}");
        
        if (request.Page.HasValue)
            queryParams.Add($"page={request.Page.Value}");

        if (request.PerPage.HasValue)
            queryParams.Add($"per_page={request.PerPage.Value}");

        string endpoint = "v1/players";
        if (queryParams.Any())
            endpoint += "?" + string.Join("&", queryParams);

        var jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, cancellationToken);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var data) != true)
            return new GetAllPlayersResponse([]);

        List<NbaPlayers> players = new List<NbaPlayers>();
        foreach (var playerElement in data.EnumerateArray())
        {
            var teamElement = playerElement.GetProperty("team");
            var team = new NbaTeams(
                teamElement.GetProperty("id").GetInt32(),
                teamElement.GetProperty("conference").GetString() is string c && !string.IsNullOrWhiteSpace(c) ? c : " -- ",
                teamElement.GetProperty("division").GetString() is string d && !string.IsNullOrWhiteSpace(d) ? d : "no division available",
                teamElement.GetProperty("city").GetString() is string city && !string.IsNullOrWhiteSpace(city) ? city : "team no longer has a city because it not exists",
                teamElement.GetProperty("name").GetString() ?? string.Empty,
                teamElement.GetProperty("full_name").GetString() ?? string.Empty,
                teamElement.GetProperty("abbreviation").GetString() ?? string.Empty
            );

            players.Add(new NbaPlayers(
                playerElement.GetProperty("id").GetInt32(),
                playerElement.GetProperty("first_name").GetString() ?? string.Empty,
                playerElement.GetProperty("last_name").GetString() ?? string.Empty,
                playerElement.GetProperty("position").GetString() ?? string.Empty,
                playerElement.TryGetProperty("height", out var heightProp) ? heightProp.GetString() : null,
                playerElement.TryGetProperty("weight", out var weightProp) ? weightProp.GetString() : null,
                playerElement.TryGetProperty("jersey_number", out var jerseyProp) ? jerseyProp.GetString() : null,
                playerElement.TryGetProperty("college", out var collegeProp) ? collegeProp.GetString() : null,
                playerElement.TryGetProperty("country", out var countryProp) ? countryProp.GetString() : null,
                playerElement.TryGetProperty("draft_year", out var draftYearProp) && draftYearProp.ValueKind == JsonValueKind.Number ? draftYearProp.GetInt32() : null,
                playerElement.TryGetProperty("draft_round", out var draftRoundProp) && draftRoundProp.ValueKind == JsonValueKind.Number ? draftRoundProp.GetInt32() : null,
                playerElement.TryGetProperty("draft_number", out var draftNumberProp) && draftNumberProp.ValueKind == JsonValueKind.Number ? draftNumberProp.GetInt32() : null,
                team
            ));
        }

        return new GetAllPlayersResponse(players);
    }
}
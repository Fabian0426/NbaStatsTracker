using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Globalization;
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
        if (request.per_page.HasValue)
            queryParams.Add($"per_page={request.per_page}");

        if (!string.IsNullOrWhiteSpace(request.search))
            queryParams.Add($"search={request.search}");

        if (!string.IsNullOrWhiteSpace(request.first_name))
            queryParams.Add($"search={request.first_name}");

        if (!string.IsNullOrWhiteSpace(request.last_name))
            queryParams.Add($"search={request.last_name}");

        foreach(var teamId in request.team_ids ?? [])
            queryParams.Add($"team_ids[]={teamId}");

        foreach (var playerId in request.player_ids ?? [])
            queryParams.Add($"player_ids[]={playerId}");


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
                GetStringSafe(playerElement, "height"),
                GetStringSafe(playerElement, "weight"),
                GetStringSafe(playerElement, "jersey_number"),
                GetStringSafe(playerElement, "college"),
                GetStringSafe(playerElement, "country"),
                GetIntSafe(playerElement, "draft_year"),
                GetIntSafe(playerElement, "draft_round"),
                GetIntSafe(playerElement, "draft_number"),
                team
            ));
        }

        return new GetAllPlayersResponse(players);
    }

    private static string? GetStringSafe(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind != JsonValueKind.Null)
        {
            return property.ToString();
        }
        return null;
    }

    private static int? GetIntSafe(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.Number)
        {
            return property.GetInt32();
        }
        return null;
    }
}
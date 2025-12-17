using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.Queries.GetASpecificPlayer;

public class GetASpecificPlayerHandler : IRequestHandler<GetASpecificPlayerRequest, GetASpecificPlayerResponse>
{
    private readonly IBalldontlieApiClient _apiClient;

    public GetASpecificPlayerHandler(IBalldontlieApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetASpecificPlayerResponse> Handle(
        GetASpecificPlayerRequest request,
        CancellationToken cancellationToken)
    {
        string endpoint = $"v1/players/{request.Id}";

        var jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var playerElement) != true)
            return new GetASpecificPlayerResponse([]);

        var team = new NbaTeams
        (
            playerElement.GetProperty("team").GetProperty("id").GetInt32(),
            playerElement.GetProperty("team").GetProperty("abbreviation").GetString() ?? string.Empty,
            playerElement.GetProperty("team").GetProperty("city").GetString() ?? string.Empty,
            playerElement.GetProperty("team").GetProperty("conference").GetString() ?? string.Empty,
            playerElement.GetProperty("team").GetProperty("division").GetString() ?? string.Empty,
            playerElement.GetProperty("team").GetProperty("full_name").GetString() ?? string.Empty,
            playerElement.GetProperty("team").GetProperty("name").GetString() ?? string.Empty
        );
        var player = new NbaPlayers
        (
            playerElement.GetProperty("id").GetInt32(),
            playerElement.GetProperty("first_name").GetString() ?? string.Empty,
            playerElement.GetProperty("last_name").GetString() ?? string.Empty,
            playerElement.GetProperty("position").GetString() ?? string.Empty,
            Utils.Utils.GetStringSafe(playerElement, "height"),
            Utils.Utils.GetStringSafe(playerElement, "weight"),
            Utils.Utils.GetStringSafe(playerElement, "jersey_number"),
            Utils.Utils.GetStringSafe(playerElement, "college"),
            Utils.Utils.GetStringSafe(playerElement, "country"),
            Utils.Utils.GetIntSafe(playerElement, "draft_year"),
            Utils.Utils.GetIntSafe(playerElement, "draft_round"),
            Utils.Utils.GetIntSafe(playerElement, "draft_number"),
            team
        );

        return new GetASpecificPlayerResponse([player]);
    }
}

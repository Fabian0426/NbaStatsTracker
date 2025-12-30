using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;
namespace NbaStatsTrackerBackend.Application.Queries.GetASpecificGame;

public class GetASpecificGameHandler : IRequestHandler<GetASpecificGameRequest, GetASpecificGameResponse>
{
    private readonly IBalldontlieApiClient _apiClient;

    public GetASpecificGameHandler(IBalldontlieApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetASpecificGameResponse> Handle(
        GetASpecificGameRequest request,
        CancellationToken cancellationToken)
    {
        string endpoint = $"v1/games/{request.Id}";

        JsonDocument? jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, cancellationToken);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var gameElement) != true)
            return new GetASpecificGameResponse([]);

        var homeTeam = new NbaTeams(
            gameElement.GetProperty("id").GetInt32(),
            Utils.Utils.GetStringSafe(gameElement, "conference") is string c && !string.IsNullOrWhiteSpace(c) ? c : " -- ",
            Utils.Utils.GetStringSafe(gameElement, "division") is string d && !string.IsNullOrWhiteSpace(d) ? d : "no division available",
            Utils.Utils.GetStringSafe(gameElement, "city") is string city && !string.IsNullOrWhiteSpace(city) ? city : "team no longer has a city because it not exists",
            Utils.Utils.GetStringSafe(gameElement, "name"),
            Utils.Utils.GetStringSafe(gameElement, "full_name"),
            Utils.Utils.GetStringSafe(gameElement, "abbreviation")
        );

        var visitorTeam = new NbaTeams(
            gameElement.GetProperty("id").GetInt32(),
            Utils.Utils.GetStringSafe(gameElement, "conference") is string vConf && !string.IsNullOrWhiteSpace(vConf) ? vConf : " -- ",
            Utils.Utils.GetStringSafe(gameElement, "division") is string vDiv && !string.IsNullOrWhiteSpace(vDiv) ? vDiv : "no division available",
            Utils.Utils.GetStringSafe(gameElement, "city") is string vCity && !string.IsNullOrWhiteSpace(vCity) ? vCity : "team no longer has a city because it not exists",
            Utils.Utils.GetStringSafe(gameElement, "name"),
            Utils.Utils.GetStringSafe(gameElement, "full_name"),
            Utils.Utils.GetStringSafe(gameElement, "abbreviation")
        );

        DateTime date = Utils.Utils.GetDateTimeSafe(gameElement, "date");
        DateTime dateTime = Utils.Utils.GetDateTimeSafe(gameElement, "datetime");
        if (dateTime == DateTime.MinValue) dateTime = date;


        var game = new NbaGames(
            gameElement.GetProperty("id").GetInt32(),
            date,
            gameElement.GetProperty("season").GetInt32(),
            Utils.Utils.GetStringSafe(gameElement, "status"),
            Utils.Utils.GetIntNullable(gameElement, "period"),
            Utils.Utils.GetStringSafe(gameElement, "time"),
            Utils.Utils.GetBooleanSafe(gameElement, "postseason"),
            Utils.Utils.GetIntNullable(gameElement, "home_team_score"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_team_score"),
            dateTime,
            Utils.Utils.GetIntNullable(gameElement, "home_q1"),
            Utils.Utils.GetIntNullable(gameElement, "home_q2"),
            Utils.Utils.GetIntNullable(gameElement, "home_q3"),
            Utils.Utils.GetIntNullable(gameElement, "home_q4"),
            Utils.Utils.GetIntNullable(gameElement, "home_ot1"),
            Utils.Utils.GetIntNullable(gameElement, "home_ot2"),
            Utils.Utils.GetIntNullable(gameElement, "home_ot3"),
            Utils.Utils.GetIntNullable(gameElement, "home_timeouts_remaining"),
            Utils.Utils.GetBooleanSafe(gameElement, "home_in_bonus"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_q1"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_q2"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_q3"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_q4"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_ot1"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_ot2"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_ot3"),
            Utils.Utils.GetIntNullable(gameElement, "visitor_timeouts_remaining"),
            Utils.Utils.GetBooleanSafe(gameElement, "visitor_in_bonus"),
            homeTeam,
            visitorTeam
        );

        return new GetASpecificGameResponse([game]);
    }
}

using MediatR;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.Queries.GetAllGames;

public class GetAllGamesHandler : IRequestHandler<GetAllGamesRequest, GetAllGamesResponse>
{
    private readonly IBalldontlieApiClient _apiClient;

    public GetAllGamesHandler(IBalldontlieApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetAllGamesResponse> Handle(GetAllGamesRequest request, CancellationToken cancellationToken)
    {
        List<string> queryParams = new List<string>();

        if (request.cursor.HasValue)
            queryParams.Add($"cursor={request.cursor.Value}");

        if (request.per_page.HasValue)
            queryParams.Add($"per_page={request.per_page.Value}");

        if (request.dates != null && request.dates.Any())
        {
            foreach (var date in request.dates)
            {
                queryParams.Add($"dates[]={date:yyyy-MM-dd}");
            }
        }

        if (request.seasons != null && request.seasons.Any())
        {
            foreach (var season in request.seasons)
            {
                queryParams.Add($"seasons[]={season}");
            }
        }

        if (request.team_ids != null && request.team_ids.Any())
        {
            foreach (var teamId in request.team_ids)
            {
                queryParams.Add($"team_ids[]={teamId}");
            }
        }

        if (request.postseason.HasValue)
            queryParams.Add($"postseason={request.postseason.Value.ToString().ToLower()}");

        if (request.start_date.HasValue)
            queryParams.Add($"start_date={request.start_date.Value:yyyy-MM-dd}");

        if (request.end_date.HasValue)
            queryParams.Add($"end_date={request.end_date.Value:yyyy-MM-dd}");

        string endpoint = "v1/games";
        if (queryParams.Any())
            endpoint += "?" + string.Join("&", queryParams);

        var jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, cancellationToken);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var data) != true)
            return new GetAllGamesResponse([]);

        List<NbaGames> games = new List<NbaGames>();

        foreach (var gameElement in data.EnumerateArray())
        {
            var homeTeamElement = gameElement.GetProperty("home_team");
            var homeTeam = new NbaTeams(
                homeTeamElement.GetProperty("id").GetInt32(),
                Utils.Utils.GetStringSafe(homeTeamElement, "conference") is string c && !string.IsNullOrWhiteSpace(c) ? c : " -- ",
                Utils.Utils.GetStringSafe(homeTeamElement, "division") is string d && !string.IsNullOrWhiteSpace(d) ? d : "no division available",
                Utils.Utils.GetStringSafe(homeTeamElement, "city") is string city && !string.IsNullOrWhiteSpace(city) ? city : "team no longer has a city because it not exists",
                Utils.Utils.GetStringSafe(homeTeamElement, "name"),
                Utils.Utils.GetStringSafe(homeTeamElement, "full_name"),
                Utils.Utils.GetStringSafe(homeTeamElement, "abbreviation")
            );

            var visitorTeamElement = gameElement.GetProperty("visitor_team");
            var visitorTeam = new NbaTeams(
                visitorTeamElement.GetProperty("id").GetInt32(),
                Utils.Utils.GetStringSafe(visitorTeamElement, "conference") is string vConf && !string.IsNullOrWhiteSpace(vConf) ? vConf : " -- ",
                Utils.Utils.GetStringSafe(visitorTeamElement, "division") is string vDiv && !string.IsNullOrWhiteSpace(vDiv) ? vDiv : "no division available",
                Utils.Utils.GetStringSafe(visitorTeamElement, "city") is string vCity && !string.IsNullOrWhiteSpace(vCity) ? vCity : "team no longer has a city because it not exists",
                Utils.Utils.GetStringSafe(visitorTeamElement, "name"),
                Utils.Utils.GetStringSafe(visitorTeamElement, "full_name"),
                Utils.Utils.GetStringSafe(visitorTeamElement, "abbreviation")
            );

            DateTime date = Utils.Utils.GetDateTimeSafe(gameElement, "date");
            DateTime dateTime = Utils.Utils.GetDateTimeSafe(gameElement, "datetime");
            if (dateTime == DateTime.MinValue) dateTime = date;


            games.Add(new NbaGames(
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
            ));
        }

        return new GetAllGamesResponse(games);
    }
}

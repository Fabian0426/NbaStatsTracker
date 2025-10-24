using MediatR;
using NbaStatsTrackerBackend.Domain.Entities;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;

public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsRequest, GetAllTeamsResponse>
{
    private readonly Infrastructure.Http.BalldontlieApiClient _apiClient;

    public GetAllTeamsHandler(Infrastructure.Http.BalldontlieApiClient apiClient)
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

        JsonDocument? jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, cancellationToken);

        if (jsonDocument?.RootElement.TryGetProperty("data", out var data) != true)
            return new GetAllTeamsResponse([]);

        List<Teams> teams = new List<Teams>();
        foreach (var teamElement in data.EnumerateArray())
        {
            teams.Add(new Teams(
                teamElement.GetProperty("id").GetInt32(),
                teamElement.GetProperty("conference").GetString() ?? string.Empty,
                teamElement.GetProperty("division").GetString() ?? string.Empty,
                teamElement.GetProperty("city").GetString() ?? string.Empty,
                teamElement.GetProperty("name").GetString() ?? string.Empty,
                teamElement.GetProperty("full_name").GetString() ?? string.Empty,
                teamElement.GetProperty("abbreviation").GetString() ?? string.Empty));
        }

        return new GetAllTeamsResponse(teams);
    }
}

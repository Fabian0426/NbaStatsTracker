//using MediatR;
//using NbaStatsTrackerBackend.Domain.Entities;
//using System.Text.Json;

//namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;

//public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsRequest, GetAllTeamsResponse>
//{
//    private readonly Infrastructure.Http.BalldontlieApiClient _apiClient;

//    public GetAllTeamsHandler(Infrastructure.Http.BalldontlieApiClient apiClient)
//    {
//        _apiClient = apiClient;
//    }

//    public async Task<GetAllTeamsResponse> Handle(GetAllTeamsRequest request, CancellationToken ct)
//    {
//        List<string> queryParams = new List<string>();

//        var endpoint = "v1/teams";
//        if (queryParams.Any())
//            endpoint += "?" + string.Join("&", queryParams);

//        JsonDocument? jsonDocument = await _apiClient.GetAsync<JsonDocument>(endpoint, ct);

//        if (jsonDocument?.RootElement.TryGetProperty("data", out var data) != true)
//            return new GetAllTeamsResponse([]);

//        var teams = data.EnumerateArray()
//            .Select(teamElement => new Teams
//            {
//                Id = teamElement.GetProperty("id"),
//                Conference = teamElement.GetProperty("conference").GetString() ?? string.Empty,
//                Division = teamElement.GetProperty("division").GetString() ?? string.Empty,
//                City = teamElement.GetProperty("city").GetString() ?? string.Empty,
//                Name = teamElement.GetProperty("name").GetString() ?? string.Empty,
//                FullName = teamElement.GetProperty("full_name").GetString() ?? string.Empty,
//                Abbreviation = teamElement.GetProperty("abbreviation").GetString() ?? string.Empty
//            })
//            .ToList();

//        return new GetAllTeamsResponse(teams);
//    }
//}
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NbaStatsTrackerBackend.Application.Interfaces;
using NbaStatsTrackerBackend.Infrastructure.Config;
using System.Text.Json;

namespace NbaStatsTrackerBackend.Infrastructure.Http;

public class BalldontlieApiClient : IBalldontlieApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BalldontlieApiClient> _logger;

    public BalldontlieApiClient(
        HttpClient httpClient,
        IOptions<ApiSettings> apiSettings,
        ILogger<BalldontlieApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _httpClient.BaseAddress = new Uri(apiSettings.Value.BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("Authorization", apiSettings.Value.ApiKey);
    }


    public async Task<JsonDocument?> GetAsync<T>(string endpoint, CancellationToken cancelationToken = default)
    {
        _logger.LogInformation("Fetching data from {Endpoint}", endpoint);

        try
        {
            var response = await _httpClient.GetAsync(endpoint, cancelationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancelationToken);
            return JsonDocument.Parse(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching from {Endpoint}", endpoint);
            throw;
        }
    }
}
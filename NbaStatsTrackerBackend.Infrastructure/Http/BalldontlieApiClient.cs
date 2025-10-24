using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using NbaStatsTrackerBackend.Infrastructure.Config;

namespace NbaStatsTrackerBackend.Infrastructure.Http;

public class BalldontlieApiClient
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



    public async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancelationToken = default)
    {
        _logger.LogInformation("Fetching data from {Endpoint}", endpoint);

        try
        {
            var response = await _httpClient.GetAsync(endpoint, cancelationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancelationToken);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(json, options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching from {Endpoint}", endpoint);
            throw;
        }
    }
}
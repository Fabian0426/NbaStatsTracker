using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.Interfaces;

public interface IBalldontlieApiClient
{
    Task<JsonDocument?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default);
}


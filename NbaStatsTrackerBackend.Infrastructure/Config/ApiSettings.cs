namespace NbaStatsTrackerBackend.Infrastructure.Config;

public class ApiSettings
{
    public const string SectionName = "BalldontlieApi";
    public string BaseUrl { get; set; } = "https://api.balldontlie.io";
    public string ApiKey { get; set; } = string.Empty;
}
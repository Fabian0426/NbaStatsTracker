namespace NbaStatsTrackerBackend.Infrastructure.Config;

public class ApiSettings
{
    public const string SectionName = "BalldontlieApi";
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}
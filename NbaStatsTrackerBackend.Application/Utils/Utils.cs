using System.Text.Json;

namespace NbaStatsTrackerBackend.Application.Utils;
public class Utils
{
    public static string? GetStringSafe(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind != JsonValueKind.Null)
        {
            return property.GetString() ?? string.Empty;
        }
        return string.Empty;
    }

    public static int? GetIntSafe(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.Number)
        {
            return property.GetInt32();
        }
        return 0;
    }
}   

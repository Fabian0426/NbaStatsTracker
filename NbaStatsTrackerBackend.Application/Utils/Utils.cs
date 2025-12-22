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

    public static int? GetIntNullable(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.Number)
        {
            return property.GetInt32();
        }
        return null;
    }

    public static DateTime GetDateTimeSafe(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.String)
        {
            if (DateTime.TryParse(property.GetString(), out var date))
            {
                return date;
            }
        }
        return DateTime.MinValue;
    }

    public static Boolean GetBooleanSafe(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.String)
        {
            return property.GetBoolean();
        }
        return false;
    }
}   

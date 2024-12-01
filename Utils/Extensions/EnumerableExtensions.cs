namespace Statistics.Utils.Extensions;

/// <summary>
/// Provide extensions for Enumerable.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Converts the passed collection into a JSON string.
    /// </summary>
    /// <returns>JSON string</returns>
    public static string ToJsonString<T>(this IEnumerable<T>? collection)
    {
        if (collection == null || !collection.Any())
        {
            return "[]";
        }

        return $"['{string.Join("', '", collection)}']";
    }
    public static string ToJsonDateTimeString(this IEnumerable<DateTime>? collection)
    {
        if (collection == null || !collection.Any())
        {
            return "[]";
        }

        return $"['{string.Join("', '", collection.Select(dt => dt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss")))}']";
    }
}


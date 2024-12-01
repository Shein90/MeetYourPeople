namespace Statistics.Utils.HealthMonitoring;

public class MonitoringSettings
{
    /// <summary>
    /// Name of section in appsettings.json.
    /// </summary>
    public const string SectionName = "Monitoring";

    /// <summary>
    /// Degraded health status code.
    /// </summary>
    public int DegradedHealthStatusCode { get; set; } = 429;

    /// <summary>
    /// Application name that does monitiring.
    /// </summary>
    public string? AppName { get; set; }

    /// <summary>
    /// Url for report without details.
    /// </summary>
    public string? HealthUrl { get; set; }

    /// <summary>
    /// Url for report with details.
    /// </summary>
    public string? HealthDetailsUrl { get; set; }
}
namespace Statistics.Utils.HealthMonitoring;

/// <summary>
/// Provides interface for service that does health check functional.
/// </summary>
public interface IMonitoringService
{
    /// <summary>
    /// Requests from subscribers health reports.
    /// </summary>
    event HealthStatusHandler HealthStatusRequested;

    /// <summary>
    /// Provided urls' set for health checking.
    /// </summary>
    /// <returns>Urls' set.</returns>
    IEnumerable<string> GetMonitoringUrls();

    /// <summary>
    /// Helps constitute info string for report
    /// </summary>
    /// <returns>Formated string.</returns>
    string GetStatusString(object? statusName, object? statusInfo);
}
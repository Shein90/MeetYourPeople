namespace Statistics.Utils.HealthMonitoring;

/// <summary>
/// Data set for health check report.
/// </summary>
/// <param name="CheckingTime">Time fo report.</param>
/// <param name="HealthStatus">Health status.</param>
/// <param name="Infos">Info set.</param>
public record HealthCheckInfo(DateTime? CheckingTime, HealthStatus? HealthStatus, string[] Infos);
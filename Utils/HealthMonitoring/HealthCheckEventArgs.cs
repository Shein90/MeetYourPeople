namespace Statistics.Utils.HealthMonitoring;

/// <summary>
/// Provide empty argument for health check event.
/// If you want to add some parametr for event, do it here.
/// </summary>
public sealed class HealthCheckEventArgs : EventArgs
{
    /// <summary>
    /// <inheritdoc cref="HealthCheckEventArgs"/>
    /// </summary>
    public HealthCheckEventArgs() { }
}
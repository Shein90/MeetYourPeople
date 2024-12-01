namespace Statistics.Utils.HealthMonitoring.Models.Kafka;

/// <summary>
/// Consumer kafka health information.
/// </summary>
public sealed class ConsumerKafkaHealth
{
    /// <summary>
    /// Kafka connection state
    /// </summary>
    public ConsumerKafkaState State { get; set; } = ConsumerKafkaState.Ok;

    /// <summary>
    /// Health info
    /// </summary>
    public string? Message { get; set; } = null;
}

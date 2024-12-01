namespace Statistics.Utils.HealthMonitoring.Models.Kafka;

/// <summary>
/// Describe consumer state by the kafka state.
/// </summary>
public enum ConsumerKafkaState
{
    Ok = 0,
    Warning = 1,
    Error = 2,
}

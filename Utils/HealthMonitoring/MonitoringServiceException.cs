namespace Statistics.Utils.HealthMonitoring;

/// <summary>
/// Provides specific exception for <see cref="MonitoringServiceException">.
/// </summary>
[Serializable]
public class MonitoringServiceException : Exception
{
    /// <summary>
    /// Constructor without parameters.
    /// </summary>
    public MonitoringServiceException() { }

    /// <summary>
    /// Constructor with special message.
    /// </summary>
    /// <param name="message">Message.</param>
    public MonitoringServiceException(string message) : base(message) { }

    /// <summary>
    /// Constructor that include inner exception.
    /// </summary>
    public MonitoringServiceException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Constructor that include serialization info and streaming context.
    /// </summary>
    protected MonitoringServiceException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }
}
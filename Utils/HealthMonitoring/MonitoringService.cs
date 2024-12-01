namespace Statistics.Utils.HealthMonitoring;

/// <summary>
/// Discribe methods subscriber for health-check event.
/// </summary>
/// <param name="eventArgs"></param>
/// <returns></returns>
public delegate HealthCheckInfo HealthStatusHandler(object? sender, HealthCheckEventArgs e);

/// <summary>
/// <inheritdoc cref="IMonitoringService"/>
/// </summary>
public sealed class MonitoringService : IMonitoringService, IHealthCheck
{
    /// <summary>
    /// <inheritdoc cref="MonitoringSettings"/>
    /// </summary>
    private static MonitoringSettings? _monitoringSettings;

    /// <summary>
    /// <inheritdoc cref="ILogger"/>
    /// </summary>
    private readonly ILogger _logger;


    /// <summary>
    /// <inheritdoc cref="JsonSerializerSettings"/>
    /// </summary>
    private static JsonSerializerSettings? _serializerSettings;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public event HealthStatusHandler? HealthStatusRequested;

    /// <summary>
    /// <inheritdoc cref="MonitoringService"/>
    /// </summary>
    public MonitoringService(IOptions<MonitoringSettings> monitoringSettings,
                             ILogger<MonitoringService> logger)
    {
        _monitoringSettings = monitoringSettings.Value;
        _logger = logger;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns><inheritdoc/></returns>
    public IEnumerable<string> GetMonitoringUrls()
    {
        if (_monitoringSettings is null)
        {
            throw new MonitoringServiceException("Receiving monitoring URLs failed!");
        }

        string[] urls = new string[]
        {
            _monitoringSettings.HealthUrl ?? "api/health",
            _monitoringSettings.HealthDetailsUrl ?? "api/health/details"
        };

        _logger.LogInformation("Healts urls have been received: {urls}.", string.Join(", ", urls));

        return urls;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns><inheritdoc/></returns>
    public string GetStatusString(object? statusName, object? statusInfo) => $"{statusName} : {statusInfo}";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (HealthStatusRequested is null)
        {
            return Task.FromResult(HealthCheckResult.Healthy("The service is stable, but not all internal components are initialized уet."));
        }

        Dictionary<string, object> healthInfos = new();

        var invocationList = HealthStatusRequested.GetInvocationList();

        foreach (var handler in invocationList)
        {
            var currentMethod = handler.Method;
            var callerObject = handler.Target;
            var serviceHealthResult = currentMethod.Invoke(callerObject, new object[] { this, new HealthCheckEventArgs() });

            var serviceName = callerObject?.GetType().Name;

            if (serviceHealthResult is HealthCheckInfo healthInfoSet)
            {
                healthInfos.Add(serviceName!, healthInfoSet);
            }
            else
            {
                healthInfos.Add(serviceName!, new HealthCheckInfo(DateTime.UtcNow, null, new[] { "Info not recieved." }));
            }
        }

        HealthCheckResult result;

        var infos = healthInfos.Values.Cast<HealthCheckInfo>();

        if (infos.Any(info => info.HealthStatus == HealthStatus.Unhealthy))
        {
            result = HealthCheckResult.Unhealthy("Service Unhealthy! Check details for more information.", data: healthInfos);
        }
        else if (infos.Any(info => info.HealthStatus == HealthStatus.Degraded))
        {
            result = HealthCheckResult.Degraded("Service Degraded! Check details for more information.", data: healthInfos);
        }
        else
        {
            result = HealthCheckResult.Healthy("Service Healthy!", data: healthInfos);
        }

        healthInfos.Add("ApplicationName", _monitoringSettings?.AppName ?? "Unknown");

        return Task.FromResult(result);
    }

    /// <summary>
    /// Provided resonce writing logic.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="MonitoringServiceException"></exception>
    public static async Task WriteResponse(HttpContext context, HealthReport healthReport)
    {
        try
        {
            var path = context.Request.Path.Value;
            var data = healthReport.Entries.FirstOrDefault().Value.Data;

            if (data.Count == 0)
            {
                data = new Dictionary<string, object>
                {
                    { "message: ", healthReport?.Entries?.FirstOrDefault().Value.Description ?? string.Empty}
                };
            }

            if (path is null || data is null)
            {
                throw new ArgumentException($"Part is [{path}], data is [{data}]");
            }

            context.Response.ContentType = "application/json; charset=utf-8";

            _serializerSettings ??= new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new Newtonsoft.Json.Converters.StringEnumConverter()
                },
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(false, false)
                },
                Formatting = Formatting.Indented
            };

            if (path.EndsWith(_monitoringSettings?.HealthUrl!, StringComparison.OrdinalIgnoreCase))
            {
                await context.Response.WriteAsync(healthReport?.Status.ToString());
            }
            else if (path.EndsWith(_monitoringSettings?.HealthDetailsUrl!, StringComparison.OrdinalIgnoreCase))
            {
                var jsonResult = JsonConvert.SerializeObject(data, _serializerSettings);

                await context.Response.WriteAsync(jsonResult);
            }
        }
        catch (Exception exception)
        {
            throw new MonitoringServiceException("The service failed to output messages.", exception);
        }
    }
}
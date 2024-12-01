namespace Common.Services;

public sealed class EventManager(ILogger<EventManager> logger) : IEventManager
{
    private readonly ILogger _logger = logger;
}
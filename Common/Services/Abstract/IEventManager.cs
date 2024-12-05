namespace Common.Services.Abstract;

public interface IEventManager
{
    Task CreateEventAsync(EventDto eventDto);

    Task<IEnumerable<EventDto>> GetAllEventsAsync();

    Task JoinEventAsync(int eventId, int userId);

    Task LeaveEventAsync(int eventId, int userId);
    Task DeleteEventAsync(int eventId);
}

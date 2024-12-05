using Common.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services.Abstract;

/// <summary>
/// Provides special functional for consume message from any message broker.
/// </summary>
public interface IEventManager
{
    Task CreateEventAsync(EventDto eventDto);

    Task<IEnumerable<EventDto>> GetAllEventsAsync();
}

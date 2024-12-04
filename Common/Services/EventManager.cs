using Common.DataTransferObjects;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Common.Services;

public sealed class EventManager(ILogger<EventManager> logger,
                                 IWebHostEnvironment env) : IEventManager
{
    private readonly ILogger<IEventManager> _logger = logger;
    private readonly IWebHostEnvironment _env = env;

    public async Task<EventDto> CreateEvent(EventDto eventDto)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath, "images/events");

        // Убедитесь, что папка существует
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(eventDto.EventImage.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await eventDto.EventImage.CopyToAsync(fileStream);
        }

        var fileUrl = $"/images/events/{uniqueFileName}";

        return eventDto.GetEventForFront(fileUrl);
    }
}
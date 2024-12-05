using Common.DataTransferObjects;
using DataAccess.DataContext;
using Domain.Event;
using Domain.Location;
using Domain.UserDomain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services;

public sealed class EventManager(ILogger<EventManager> logger,
                                 IWebHostEnvironment env,
                                 MypDbContext db) : IEventManager
{
    private readonly ILogger<IEventManager> _logger = logger;
    private readonly IWebHostEnvironment _env = env;
    private readonly MypDbContext _dbContext = db;

    public async Task CreateEventAsync(EventDto eventDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == eventDto.OwnerId)
            ?? throw new Exception("User not found!");

        var address = new Address
        {
            AddressText = eventDto.Address
        };

        var dt = DateTime.Parse($"{eventDto.Date} {eventDto.Time}");

        var meeting = new Meeting()
        {
            DateTime = dt,
            Title = eventDto.Title,
            Description = eventDto.Description,
            DetailedDescription = eventDto.DetailedDescription,
            MaxParticipants = eventDto.MaxParticipants,
            Address = address,
        };

        var photoPath = await SaveEventPhotoAsync(eventDto);

        var photo = new MeetingPhoto()
        {
            Meeting = meeting,
            PhotoUrl = photoPath,
            UploadDateTime = DateTime.UtcNow
        };

        var meetingArrangement = new MeetingArrangement()
        {
            Meeting = meeting,
            User = user,
            UserRole = UserMeetingRole.Owner
        };

        await _dbContext.MeetingPhotos.AddAsync(photo);
        await _dbContext.MeetingArrangements.AddAsync(meetingArrangement);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var allMeetings = await _dbContext.Meetings
            .Include(m => m.MeetingPhotos)
            .Include(m => m.MeetingArrangements)
                .ThenInclude(ma => ma.User)
            .Include(m => m.Address)
            .AsNoTracking()
            .ToListAsync();

        var events = allMeetings.Select(meeting => new EventDto
        {
            Id = meeting.Id,
            OwnerId = meeting.MeetingArrangements
                    .FirstOrDefault(ma => ma.UserRole == UserMeetingRole.Owner)?.UserId ?? 0,
            OwnerName = meeting.MeetingArrangements
                    .FirstOrDefault(ma => ma.UserRole == UserMeetingRole.Owner)?.User.UserName,
            Title = meeting.Title,
            Description = meeting.Description,
            DetailedDescription = meeting.DetailedDescription,
            Date = meeting.DateTime.ToString("dd-MM-yyyy"),
            Time = meeting.DateTime.ToString("HH:mm"),
            Address = meeting.Address.AddressText,
            MaxParticipants = meeting.MaxParticipants,
            Participants = meeting.MeetingArrangements.Count,
            EventImageUrl = meeting.MeetingPhotos.FirstOrDefault()?.PhotoUrl ?? string.Empty,
            EventImage = null
        });

        return events;
    }

    private async Task<string> SaveEventPhotoAsync(EventDto eventDto)
    {

        var uploadsFolder = Path.Combine(_env.WebRootPath, "images/events");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(eventDto.EventImage?.FileName);

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        if (eventDto.EventImage != null)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await eventDto.EventImage.CopyToAsync(fileStream);
        }
        else
        {
            throw new Exception("File not found");
        }

        return $"/images/events/{uniqueFileName}";
    }
}
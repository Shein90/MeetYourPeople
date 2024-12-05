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

    public async Task JoinEventAsync(int eventId, int userId)
    {
        var meeting = await _dbContext.Meetings
            .FirstOrDefaultAsync(m => m.Id == eventId) 
            ?? throw new Exception($"Meeting not found! MeetingId: {eventId}");

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new Exception($"User not found! UserId: {userId}");

        if (meeting.MeetingArrangements.Any(ma => ma.UserId == userId))
        {
            throw new InvalidOperationException($"User already registered: {userId}");
        }

        var meetingArrangement = new MeetingArrangement()
        {
            UserId = user.Id,
            MeetingId = meeting.Id,
            UserRole = UserMeetingRole.Participant
        };

        await _dbContext.MeetingArrangements.AddAsync(meetingArrangement);

        await _dbContext.SaveChangesAsync();
    }

    public async Task LeaveEventAsync(int eventId, int userId)
    {
        var meetingArrangement = await _dbContext.MeetingArrangements
            .FirstOrDefaultAsync(ma => ma.MeetingId == eventId && ma.UserId == userId)
            ?? throw new Exception("User not found!");

        if (meetingArrangement.UserRole == UserMeetingRole.Owner)
        {
            throw new InvalidOperationException("Owner cannot leave the event. Transfer ownership first.");
        }

        _dbContext.MeetingArrangements.Remove(meetingArrangement);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(int eventId)
    {
        var meeting = await _dbContext.Meetings
            .FirstOrDefaultAsync(m => m.Id == eventId)
            ?? throw new Exception($"Meeting not found! MeetingId: {eventId}");

        foreach (var photo in meeting.MeetingPhotos)
        {
            var photoFilePath = Path.Combine(_env.WebRootPath, photo.PhotoUrl.TrimStart('/'));

            if (File.Exists(photoFilePath))
            {
                File.Delete(photoFilePath);
            }
        }

        _dbContext.MeetingArrangements.RemoveRange(meeting.MeetingArrangements);
        _dbContext.MeetingPhotos.RemoveRange(meeting.MeetingPhotos);

        _dbContext.Meetings.Remove(meeting);

        await _dbContext.SaveChangesAsync();
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
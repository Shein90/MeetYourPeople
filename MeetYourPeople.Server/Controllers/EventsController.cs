using Domain.Meeting;
using Domain.UserDomain;
using Microsoft.AspNetCore.Mvc;

namespace MeetYourPeople.Server.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController(ILogger<EventsController> logger) : ControllerBase
    {
        private readonly ILogger<EventsController> _logger = logger;

        [HttpGet]
        public IEnumerable<EventDTO> Get()
        {
            var testObjects = Enumerable.Range(1, 5).Select(index => new Meeting()
            {
                MeetingID = index,
                MeetingOwnerID = index,
                AddressID = index,
                DateTime = DateTime.Now.AddDays(5), // Будущая дата для теста
                Title = "Project Kickoff",
                Description = "Initial project meeting.",
                DetailedDescription = "This meeting is intended to discuss the initial phases of the project and align goals.",
                MaxParticipants = 10,
                MeetingOwner = new User
                {
                    UserID = 1,
                    AddressID = 1,
                    UserName = "johndoe",
                    Email = "johndoe@example.com",
                    PasswordHash = "hashedpassword123",
                    DateOfBirth = new DateTime(1990, 5, 23),
                    Address = new Address
                    {
                        AddressID = 1,
                        AddressText = "123 Main Street"
                    }
                },
                Address = new Address
                {
                    AddressID = 1,
                    AddressText = "456 Meeting Street"
                },
                MeetingPhotos = null
            });

            var res = testObjects.Select(meeting => new EventDTO
            {
                Id = meeting.MeetingID,
                MeetingOwnerID = meeting.MeetingOwnerID,
                Title = meeting.Title,
                Description = meeting.Description,
                DetailedDescription = meeting.DetailedDescription,
                DateTime = meeting.DateTime.ToString("dd.MM.yyyy 'at' HH.mm"),
                Address = meeting.Address.AddressText,
                MeetingOwnerName = meeting.MeetingOwner.UserName
            });

            return res.ToArray();
        }
    }
}

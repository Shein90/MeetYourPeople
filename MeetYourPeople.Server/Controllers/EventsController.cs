using Domain.Event;
using Domain.UserDomain;
using Domain.Location;
using Microsoft.AspNetCore.Mvc;

namespace MeetYourPeople.Server.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController(ILogger<EventsController> logger) : ControllerBase
    {
        private readonly ILogger<EventsController> _logger = logger;

        [HttpGet]
        public IEnumerable<EventDto> Get()
        {
            var testObjects = Enumerable.Range(1,10).Select(index => new Meeting()
            {
                Id = index,
                AddressID = index,
                DateTime = DateTime.Now.AddDays(5),
                Title = "Project Kickoff",
                Description = "Initial project meeting.",
                DetailedDescription = "This meeting is intended to discuss the initial phases of the project and align goals.",
                MaxParticipants = 10,
                Address = new Address
                {
                    Id = 1,
                    AddressText = "456 Meeting Street"
                },
                MeetingPhotos = null
            });

            var res = testObjects.Select(meeting => new EventDto
            {
                Id = meeting.Id,
  
                Title = meeting.Title,
                Description = meeting.Description,
                DetailedDescription = meeting.DetailedDescription,
                DateTime = meeting.DateTime.ToString("dd.MM.yyyy 'at' HH.mm"),
                Address = meeting.Address.AddressText,
                MaxParticipants = meeting.MaxParticipants

            });

            return res.ToArray();
        }
    }
}

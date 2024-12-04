using Common.DataTransferObjects;
using Domain.Event;
using Domain.Location;

namespace MeetYourPeople.Server.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController(ILogger<EventsController> logger,
                                  IEventManager eventManager) : ControllerBase
    {
        private readonly ILogger<EventsController> _logger = logger;
        private readonly IEventManager _eventManager = eventManager;

        [HttpGet]
        public IEnumerable<EventDto> Get()
        {
            //var testObjects = Enumerable.Range(1, 10).Select(index => new Meeting()
            //{
            //    Id = index,
            //    AddressId = index,
            //    DateTime = DateTime.Now.AddDays(5),
            //    Title = "Project Kickoff",
            //    Description = "Initial project meeting.",
            //    DetailedDescription = "This meeting is intended to discuss the initial phases of the project and align goals.",
            //    MaxParticipants = 10,
            //    Address = new Address
            //    {
            //        Id = 1,
            //        AddressText = "456 Meeting Street"
            //    },
            //    MeetingPhotos = null
            //});

            //var res = testObjects.Select(meeting => new EventDto
            //{
            //    Id = meeting.Id,

            //    Title = meeting.Title,
            //    Description = meeting.Description,
            //    DetailedDescription = meeting.DetailedDescription,
            //    DateTime = meeting.DateTime.ToString("dd.MM.yyyy 'at' HH.mm"),
            //    Address = meeting.Address.AddressText,
            //    MaxParticipants = meeting.MaxParticipants

            //});

            //return res.ToArray();
            return [];
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromForm] EventDto eventDto)
        {
            try
            {
                if (!eventDto.EventImage.ContentType.StartsWith("image/"))
                {
                    return BadRequest("Only image files are allowed.");
                }

                await _eventManager.CreateEvent(eventDto);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Event creation ERROR: {Message}", ex.Message);

                return StatusCode(500, ex.Message);
            }
           
        }
    }
}

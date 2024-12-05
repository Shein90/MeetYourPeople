using Common.DataTransferObjects;
using Domain.Event;
using Domain.Location;

namespace MeetYourPeople.Server.Controllers
{
    [ApiController]
    [Route("api/events")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class EventsController(ILogger<EventsController> logger,
                                  IEventManager eventManager) : ControllerBase
    {
        private readonly ILogger<EventsController> _logger = logger;
        private readonly IEventManager _eventManager = eventManager;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get()
        {
            try
            {
                var events = await _eventManager.GetAllEventsAsync();

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError("Event getting ERROR: {Message}", ex.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEvent([FromForm] EventDto eventDto)
        {
            try
            {
                if (!eventDto.EventImage?.ContentType.StartsWith("image/") ?? false)
                {
                    return BadRequest("Only image files are allowed.");
                }

                await _eventManager.CreateEventAsync(eventDto);

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

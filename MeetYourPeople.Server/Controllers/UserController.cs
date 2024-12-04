using Domain.UserDomain;

namespace MeetYourPeople.Server.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ILogger<UserController> logger,
                            IUserManager userManager) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUserManager _userManager = userManager;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            var response = await _userManager.RegisterNewUserAsync(user);

            _logger.LogInformation("Register user: {user}", user);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Register User ERROR: {Message}", ex.Message);

            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var response = await _userManager.LogIn(loginRequest);

            _logger.LogInformation("Login user: {user}", response.User);

            return Ok(response);
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Login User ERROR: {Message}", ex.Message);

            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> CheckUserAuth()
    {
        try
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var checkedUser = await _userManager.CheckUserAuth(token);

            _logger.LogInformation("Register user: {user}", checkedUser);

            return Ok(checkedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError("CheckUserAuth ERROR: {Message}", ex.Message);

            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            _logger.LogInformation("Updated user: {Email}", user);

            var updatedUser = await _userManager.UpdateUserAsync(user);

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError("CheckUserAuth ERROR: {Message}", ex.Message);

            return StatusCode(500, ex.Message);
        }
    }
}
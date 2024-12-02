using Common.Authentication;
using Domain.UserDomain;

namespace MeetYourPeople.Server.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ILogger<UserController> logger,
                            IUserManager userManager,
                            IOptions<JwtAuthenticationSettings> jwtSettings) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUserManager _userManager = userManager;
    private readonly JwtAuthenticationSettings _jwtSettings = jwtSettings.Value;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest("User data is required.");

        var response = await _userManager.RegisterNewUserAsync(userDto);

        return Ok(response);
    }

    // Обновление данных существующего пользователя
    [HttpPut("update")]
    [Authorize] // Требует авторизации
    public IActionResult Update([FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest("User data is required.");

        // Логика для обновления данных пользователя (например, обновление в БД)
        _logger.LogInformation("Updating user: {Email}", userDto.Email);

        // Возвращаем обновленные данные пользователя
        var updatedUser = new
        {
            userDto.Email,
            userDto.UserName,
            userDto.DateOfBirth,
            userDto.Address
        };

        return Ok(new { user = updatedUser });
    }

    [Authorize]
    [HttpGet("check-auth")]
    public IActionResult GetProtectedResource()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        var principal = _jwtSettings.ValidateJwtToken(token);

        var userIdClaim = principal?.Claims.FirstOrDefault(c => c.Type == "id");

        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token");
        }

        //var user = _dbContext.Users.Find(userId);   // Ищем пользователя в БД

        //if (user == null)
        //    return NotFound("User not found");



        //return Ok(user);
        return Ok();
    }
}

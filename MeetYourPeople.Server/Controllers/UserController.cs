using Common.Authentication;
using Domain.UserDomain;

namespace MeetYourPeople.Server.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ILogger<UserController> logger,
                            IUserManager userManager) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUserManager _userManager = userManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest("User data is required.");

        var response = await _userManager.RegisterNewUserAsync(userDto);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("check-auth")]
    public IActionResult CheckUserAuth()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        var user = await _userManager.CheckUserAuth(token);

        return Ok(user);
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


}

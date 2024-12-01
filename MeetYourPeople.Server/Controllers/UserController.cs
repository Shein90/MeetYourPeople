using Domain.UserDomain;
using Microsoft.AspNetCore.Mvc;

namespace MeetYourPeople.Server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(ILogger<UserController> logger, MypDbContext db) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        MypDbContext _db = db;

        // Регистрация нового пользователя
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var s = _db.Addresses;
            if (userDto == null)
                return BadRequest("User data is required.");

            // Логика для создания нового пользователя (например, запись в БД)
            _logger.LogInformation("Registering new user: {Email}", userDto.Email);

            // Возвращаем токен и данные пользователя в ответ
            var token = "dummy_token"; // Здесь должна быть логика генерации токена
            var response = new
            {
                token,
                user = new
                {
                    userDto.Email,
                    userDto.UserName,
                    userDto.DateOfBirth,
                    userDto.Address
                }
            };

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
    }
}

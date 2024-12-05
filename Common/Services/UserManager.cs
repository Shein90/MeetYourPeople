using Common.Authentication;
using Common.DataTransferObjects;
using DataAccess;
using DataAccess.DataContext;
using Domain.Location;
using Domain.UserDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services;

public sealed class UserManager(ILogger<UserManager> logger,
                                MypDbContext db,
                                IPasswordService passwordService,
                                IOptions<JwtAuthenticationSettings> jwtSettings) : IUserManager
{
    private readonly ILogger<IUserManager> _logger = logger;
    private readonly MypDbContext _dbContext = db;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly JwtAuthenticationSettings _jwtSettings = jwtSettings.Value;

    public async Task<AuthResponseDto> RegisterNewUserAsync(UserDto userDto)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);

        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        var address = new Address
        {
            AddressText = userDto.Address
        };

        var user = new User
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            DateOfBirth = userDto.DateOfBirth,
            AddressId = address.Id,
            Address = address,
            PasswordHash = _passwordService.GetHashFromPassword(userDto.Password!)
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var token = _jwtSettings.GenerateJwtToken(user);

        userDto.Id = user.Id;

        var authResponse = new AuthResponseDto()
        {
            Token = token,
            User = userDto
        };

        _logger.LogInformation("Registering new user. Response data: {authResponse}", authResponse);

        return authResponse;
    }

    public async Task<UserDto> CheckUserAuth(string token)
    {
        var principal = _jwtSettings.ValidateJwtToken(token);

        var userIdClaim = principal?.Claims.FirstOrDefault(c => c.Type == "id");

        if (int.TryParse(userIdClaim?.Value, out int id))
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id)
                                             ?? throw new Exception("User not found!");

            return UserDto.GetDtoFromUser(user);
        }
        else
        {
            throw new Exception("Incorrect User Id!");
        }
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id)
                                   ?? throw new Exception("User not found!");

        user.UserName = userDto.UserName ?? user.UserName;
        user.Email = userDto.Email ?? user.Email;
        user.DateOfBirth = userDto.DateOfBirth == default ? user.DateOfBirth : userDto.DateOfBirth;
        user.Address.AddressText = userDto.Address;

        if (!_passwordService.VerifyPassword(user.PasswordHash, userDto?.Password ?? string.Empty))
        {
            user.PasswordHash = _passwordService.GetHashFromPassword(userDto?.Password ?? string.Empty);
        }

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return UserDto.GetDtoFromUser(user);
    }

    public async Task<AuthResponseDto> LogIn(LoginRequest loginRequest)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email)
                                   ?? throw new UnauthorizedAccessException("User not found!");

        if (!_passwordService.VerifyPassword(user.PasswordHash, loginRequest.Password))
        {
            throw new UnauthorizedAccessException("Wrong password!");
        }

        var token = _jwtSettings.GenerateJwtToken(user);

        var authResponse = new AuthResponseDto()
        {
            Token = token,
            User = UserDto.GetDtoFromUser(user)
        };

        authResponse.User.Password = loginRequest.Password;

        _logger.LogInformation("Registering new user. Response data: {authResponse}", authResponse);

        return authResponse;
    }
}


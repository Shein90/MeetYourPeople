using Common.Authentication;
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
    private readonly ILogger _logger = logger;
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

        await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();


        var user = new User
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            DateOfBirth = userDto.DateOfBirth,
            AddressId = address.Id,
            Address = address,
            PasswordHash = _passwordService.GetHashFromPassword(userDto.Password)
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var token = _jwtSettings.GenerateJwtToken(user);

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
            var user = await _dbContext.Users.Include(u => u.Address)
                                             .FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return null!;
            }

            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address.AddressText,
                DateOfBirth = user.DateOfBirth
            };
        }
        else
        {
            return null!;
        }
    }

    public Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        return Task.FromResult(new UserDto());
    }
}


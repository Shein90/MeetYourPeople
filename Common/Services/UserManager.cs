using Common.Authentication;
using DataAccess;
using Domain.Location;
using Domain.UserDomain;
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
        var existingUser =  _dbContext.Users;//.FirstOrDefaultAsync(u => u.Email == userDto.Email);

        //if (existingUser != null)
        //{
        //    throw new Exception("User with this email already exists.");
        //}

        //var existingUser = _dbContext.Users;

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
            AddressID = address.Id,
            Address = address,
            PasswordHash = _passwordService.HashPassword(userDto.Password)
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var token = _jwtSettings.GenerateJwtToken(userDto);

        var authResponse = new AuthResponseDto()
        {
            Token = token,
            User = new UserDto()
            {
                Email = user.Email,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address.AddressText,
                Password = userDto.Password
            }
        };

        _logger.LogInformation("Registering new user. Response data: {authResponse}", authResponse);

        return authResponse;
    }

}
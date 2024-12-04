using Domain.UserDomain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Common.Services.Abstract;

public interface IUserManager
{
    Task<AuthResponseDto> RegisterNewUserAsync(UserDto user);

    Task<AuthResponseDto> LogIn(LoginRequest loginRequest);

    Task<UserDto> CheckUserAuth(string token);

    Task<UserDto> UpdateUserAsync(UserDto user);
}
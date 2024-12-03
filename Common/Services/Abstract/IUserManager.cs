using Domain.UserDomain;
using System.Threading.Tasks;

namespace Common.Services.Abstract;

public interface IUserManager
{
    Task<AuthResponseDto> RegisterNewUserAsync(UserDto userDto);

    Task<UserDto> CheckUserAuth(string token);

    Task<UserDto> UpdateUserAsync(UserDto userDto);
}
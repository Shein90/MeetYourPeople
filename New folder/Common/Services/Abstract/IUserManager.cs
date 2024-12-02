using Domain.UserDomain;
using System.Threading.Tasks;

namespace Common.Services.Abstract;

public interface IUserManager
{
    Task<AuthResponseDto> RegisterNewUserAsync(UserDto userDto);
}
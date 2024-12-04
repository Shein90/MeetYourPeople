using Domain.UserDomain;

namespace Common.DataTransferObjects;

public sealed record UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }

    public static UserDto GetDtoFromUser(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            DateOfBirth = user.DateOfBirth,
            Address = user.Address.AddressText
        };
    }
}

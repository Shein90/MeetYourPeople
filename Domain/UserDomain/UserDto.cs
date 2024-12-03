namespace Domain.UserDomain;

public sealed record UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
}

namespace Domain.UserDomain;

public sealed record UserDto
{
    public int UserID { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required string Address { get; set; }
    public required string Password { get; set; }
}

namespace Domain.UserDomain;

public record class User
{
    public int Id { get; init; }
    public required string UserName { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }

    public int AddressID { get; init; }
    public required Address Address { get; init; }
    public ICollection<MeetingArrangement>? MeetingArrangements { get; init; }
}
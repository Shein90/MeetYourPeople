namespace Domain.UserDomain;

public record class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int AddressId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<MeetingArrangement> MeetingArrangements { get; set; } = [];
}
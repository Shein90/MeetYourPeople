namespace Domain.Location;

public record class Address
{
    public int Id { get; set; }
    public string AddressText { get; set; } = null!;
    public virtual ICollection<Meeting> Meetings { get; set; } = [];
    public virtual ICollection<User> Users { get; set; } = [];
}
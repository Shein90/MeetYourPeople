namespace Domain.Location;

public record class Address
{
    public int Id { get; init; }
    public required string AddressText { get; init; }

    public ICollection<User>? Users { get; init; }
    public ICollection<Meeting>? Meetings { get; init; }
}
using Domain.UserDomain;

namespace Domain.Meeting;

public record class Address
{
    public int AddressID { get; init; }
    public required string AddressText { get; init; }

    public ICollection<User>? Users { get; init; }
    public ICollection<Meeting>? Meetings { get; init; }
}
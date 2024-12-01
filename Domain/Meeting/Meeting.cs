using Domain.UserDomain;
using System.Text.Json.Serialization;

namespace Domain.Meeting;

public record class Meeting
{
    public int MeetingID { get; init; }
    public int MeetingOwnerID { get; init; }
    public int AddressID { get; init; }
    public DateTime DateTime { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }

    public required string DetailedDescription { get; init; }
    public int MaxParticipants { get; init; }
    public required User MeetingOwner { get; init; }
    public required Address Address { get; init; }

    public ICollection<MeetingPhoto>? MeetingPhotos { get; init; }
    public ICollection<MeetingArrangement>? MeetingArrangements { get; init; }
}

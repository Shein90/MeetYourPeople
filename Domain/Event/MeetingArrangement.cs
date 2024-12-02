namespace Domain.Event;

public record class MeetingArrangement
{
    public int Id { get; init; }
    public UserMeetingRole UserRole { get; init; }

    public int UserID { get; init; }
    public int MeetingID { get; init; }
    public required User User { get; init; }
    public required Meeting Meeting { get; init; }
}
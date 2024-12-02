namespace Domain.Event;

public record class MeetingPhoto
{
    public int Id { get; init; }
    public DateTime UploadDateTime { get; init; }
    public required string PhotoURL { get; init; }

    public int MeetingID { get; init; }
    public required Meeting Meeting { get; init; }
}
namespace Domain.Event;

public record class MeetingPhoto
{
    public int Id { get; set; }
    public DateTime UploadDateTime { get; set; }
    public string PhotoUrl { get; set; } = null!;
    public int MeetingId { get; set; }
    public virtual Meeting Meeting { get; set; } = null!;
}

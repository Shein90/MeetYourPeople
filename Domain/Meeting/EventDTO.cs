namespace Domain.Meeting;
public sealed record EventDTO
{
    public int Id { get; set; }
    public int MeetingOwnerID { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string DetailedDescription { get; set; }
    public required string DateTime { get; set; }
    public required string Address { get; set; }
    public required string MeetingOwnerName { get; set; }
}
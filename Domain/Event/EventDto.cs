namespace Domain.Event;
public sealed record EventDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string DetailedDescription { get; set; }
    public required string DateTime { get; set; }
    public required string Address { get; set; }
    public required int MaxParticipants { get; set; }
}
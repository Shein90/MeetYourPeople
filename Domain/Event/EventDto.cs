namespace Domain.Event;
public sealed record EventDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string DetailedDescription { get; set; }
    public string DateTime { get; set; }
    public string Address { get; set; }
    public int MaxParticipants { get; set; }
}
namespace Common.DataTransferObjects;

public sealed record EventDto
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string DetailedDescription { get; set; }
    public required string Date { get; set; }
    public required string Time { get; set; }
    public required string Address { get; set; }
    public int MaxParticipants { get; set; }
    public int? Participants { get; set; }
    public IFormFile? EventImage { get; set; }
    public string? EventImageUrl { get; set; }
}
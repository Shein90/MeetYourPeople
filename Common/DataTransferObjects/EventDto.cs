namespace Common.DataTransferObjects;

public sealed record EventDto
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string DetailedDescription { get; set; }
    public required string Date { get; set; }
    public required string Time { get; set; }
    public required string Address { get; set; }
    public int MaxParticipants { get; set; }
    public IFormFile EventImage { get; set; }
    public string? EventImageUrl { get; set; }

    public EventDto GetEventForFront(string imageUrl)
    {
        return new EventDto()
        {
            Id = this.Id,
            OwnerId = this.OwnerId,
            Title = this.Title,
            Description = this.Description,
            DetailedDescription = this.DetailedDescription,
            Date = this.Date,
            Time = this.Time,
            Address = this.Address,
            MaxParticipants = this.MaxParticipants,
            EventImage = null!,
            EventImageUrl = imageUrl
        };
    }
}
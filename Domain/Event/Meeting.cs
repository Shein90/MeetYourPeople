﻿namespace Domain.Event;

public record class Meeting
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DetailedDescription { get; set; } = null!;
    public int MaxParticipants { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<MeetingArrangement> MeetingArrangements { get; set; } = [];
    public virtual ICollection<MeetingPhoto> MeetingPhotos { get; set; } = [];
}

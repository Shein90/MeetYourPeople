using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Meeting
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DetailedDescription { get; set; } = null!;

    public int MaxParticipants { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Meetingarrangement> MeetingArrangements { get; set; } = new List<Meetingarrangement>();

    public virtual ICollection<Meetingphoto> MeetingPhotos { get; set; } = new List<Meetingphoto>();
}

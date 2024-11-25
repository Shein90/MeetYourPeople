using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Meeting;

public class Meeting
{
    public int Id { get; set; }
    public DateTime DateTimeTitle { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int MaxParticipants { get; set; } = 10;

    public required ApplicationUser MeetingOwner { get; set; }
    public required Address Address { get; set; }
    public ICollection<MeetingPhoto> MeetingPhotos { get; set; }
    public ICollection<MeetingArrangement> MeetingArrangements { get; set; }
}

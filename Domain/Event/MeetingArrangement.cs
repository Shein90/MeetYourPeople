﻿namespace Domain.Event;

public partial class MeetingArrangement
{
    public int Id { get; set; }

    public UserMeetingRole UserRole { get; set; }

    public int UserId { get; set; }

    public int MeetingId { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
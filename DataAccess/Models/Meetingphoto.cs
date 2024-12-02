using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Meetingphoto
{
    public int Id { get; set; }

    public DateTime UploadDateTime { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public int MeetingId { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Meeting;

public class MeetingPhoto
{
    public int Id { get; set; }
    public required DateTime UploadDateTime { get; set; }
    public required string PhotoURL { get; set; }
    public required Meeting Meeting { get; set; }
}

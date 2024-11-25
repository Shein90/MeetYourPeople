using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Meeting;

public class MeetingArrangement
{
    public int Id { get; set; }
    public required ApplicationUser User { get; set; }
    public required Meeting Meeting { get; set; }
}

using Domain.Meeting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User;

public class ApplicationUser : IdentityUser
{
    public DateTime DateOfBirth { get; private set; }

    // Навигационные свойства
    public ICollection<UserPhoto> UserPhotos { get; set; }
    public ICollection<MeetingArrangement> MeetingArrangements { get; set; }
}

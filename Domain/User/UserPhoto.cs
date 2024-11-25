using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User;

public class UserPhoto
{
    public int Id { get; set; }
    public DateTime UploadDateTime { get; set; }
    public required string PhotoURL { get; set; }

    public required ApplicationUser User { get; set; }
}


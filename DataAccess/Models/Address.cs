using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Address
{
    public int Id { get; set; }

    public string AddressText { get; set; } = null!;

    public virtual ICollection<Meeting> Meetings { get; set; } = [];

    public virtual ICollection<User> Users { get; set; } = [];
}

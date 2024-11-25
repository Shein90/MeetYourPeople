using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Meeting;

public class Address
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string Suburb { get; set; }
    public required string PostCode { get; set; }
    public required string Street { get; set; }
    public required string BuildingNumber { get; set; }
    public string? ApartmentNumber { get; set; }

    public ICollection<Meeting> Meetings { get; set; }
}

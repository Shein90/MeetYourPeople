namespace DataAccess.Models;

public partial class Meetingarrangement
{
    public int Id { get; set; }

    public int UserRole { get; set; }

    public int UserId { get; set; }

    public int MeetingId { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

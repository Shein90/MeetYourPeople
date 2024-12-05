namespace Common.DataTransferObjects;

public sealed record UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required string Address { get; set; }
    public string? Password { get; set; }
    public required ICollection<int>? EventsIds { get; set; } = [];

    public static UserDto GetDtoFromUser(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            DateOfBirth = user.DateOfBirth,
            Address = user.Address.AddressText,
            EventsIds = user.MeetingArrangements
                .Select(ma => ma.MeetingId)
                .ToList()
        };
    }
}


namespace Common.DataTransferObjects;

public sealed record AuthResponseDto
{
    public string Token { get; init; }
    public UserDto User { get; init; }
}

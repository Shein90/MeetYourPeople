using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain;

public sealed record AuthResponseDto
{
    public required string Token { get; init; }
    public required UserDto User { get; init; }
}

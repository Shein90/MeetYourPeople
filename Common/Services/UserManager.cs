namespace Common.Services;

public sealed class UserManager(ILogger<UserManager> logger) : IUserManager
{
    private readonly ILogger _logger = logger;
}
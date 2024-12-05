namespace Common.DataTransferObjects;

public sealed record EventRegistrationRequest
{
    public int UserId { get; set; }
}
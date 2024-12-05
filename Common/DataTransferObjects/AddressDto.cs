namespace Common.DataTransferObjects;

public sealed record AddressDto
{
    public int AddressID { get; set; }
    public required string AddressText { get; set; }
}

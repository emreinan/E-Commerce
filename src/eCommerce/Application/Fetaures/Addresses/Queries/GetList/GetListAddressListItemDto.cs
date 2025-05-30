namespace Application.Fetaures.Addresses.Queries.GetList;

public class    GetListAddressListItemDto 
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public string AddressTitle { get; set; } =default!;
    public string FullName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string? ZipCode { get; set; }
    public string PhoneNumber { get; set; } = default!;
}
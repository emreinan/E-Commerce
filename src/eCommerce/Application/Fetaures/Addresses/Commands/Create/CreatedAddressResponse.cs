
namespace Application.Fetaures.Addresses.Commands.Create;

public class CreatedAddressResponse
{
    public Guid Id { get; init; }
    public Guid? UserId { get; init; }
    public string? GuestId { get; init; }
    public string AddressTitle { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
    public string Street { get; init; } = default!;
    public string District { get; init; } = default!;
    public string City { get; init; } = default!;
    public string? ZipCode { get; init; }
    public string AddressDetail { get; init; } = default!;
    public bool IsDefault { get; init; }

}
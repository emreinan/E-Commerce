
namespace Application.Fetaures.Addresses.Commands.Update;

public sealed record UpdateAddressRequest
(
    Guid? UserId,
    string AddressTitle,
    string? FullName,
    string PhoneNumber,
    string City,
    string District,
    string Street,
    string? ZipCode,
    string AddressDetail
);

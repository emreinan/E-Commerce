using Core.Persistence.Domain;

namespace Domain.Entities;

public class Address : Entity<Guid>
{
    public Guid? UserId { get; set; } 
    public string? GuestId { get; set; } 
    public string AddressTitle { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? ZipCode { get; set; }
    public string AddressDetail { get; set; } = default!;
    public bool IsDefault { get; set; }

    public virtual User? User { get; set; }
}

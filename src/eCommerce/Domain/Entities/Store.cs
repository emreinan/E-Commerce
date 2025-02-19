using Core.Persistence.Domain;

namespace Domain.Entities;

public class Store : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }

    public virtual ICollection<Product> Products { get; set; } = default!;
}

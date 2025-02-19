using Core.Persistence.Domain;

namespace Domain.Entities;

public class User : Entity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public bool IsActive { get; set; } 


    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<Address> Addresses { get; set; } = default!;
    public virtual ICollection<Order> Orders { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = default!;
    public virtual ICollection<ProductComment> ProductComments { get; set; } = default!;
    public virtual Basket? Basket { get; set; }
    public virtual Store? Store { get; set; }
    public virtual PersonalInfo? PersonalInfo { get; set; }
}

public sealed record PersonalInfo
{
    public string? TcNo { get; init; } = default!;
    public DateTime? DateOfBirth { get; init; }
    public string? ProfileImageUrl { get; init; }
}

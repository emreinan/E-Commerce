using Core.Persistence.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : Entity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public byte[] PasswordHash { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? VerificationCode { get; set; }
    public bool IsActive { get; set; } 


    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<Address> Addresses { get; set; } = default!;
    public virtual ICollection<Order> Orders { get; set; } = default!;
    public virtual ICollection<ProductComment> ProductComments { get; set; } = default!;
    public virtual Basket? Basket { get; set; }
    [NotMapped]
    public PersonalInfo? PersonalInfo { get; set; }
}

public sealed record PersonalInfo
{
    public string? TcNo { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ProfileImageUrl { get; set; }
}

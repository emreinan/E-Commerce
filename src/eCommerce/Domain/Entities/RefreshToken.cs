using Core.Persistence.Domain;

namespace Domain.Entities;

public class RefreshToken : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string CreatedByIp { get; set; } = default!;
    public string? ReplacedByToken { get; set; }
    public string? ReasonRevoked { get; set; }

    public virtual User User { get; set; } = default!;
}

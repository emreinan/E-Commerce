
using Core.Persistence.Domain;

namespace Domain.Entities;

public class UserRole : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual Role Role { get; set; } = default!;
}

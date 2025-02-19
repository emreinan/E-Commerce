
using Core.Persistence.Domain;

namespace Domain.Entities;

public class Role : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
}

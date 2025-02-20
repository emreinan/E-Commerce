
using Core.Persistence.Domain;
using Domain.Enums;

namespace Domain.Entities;

public class Role : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
}

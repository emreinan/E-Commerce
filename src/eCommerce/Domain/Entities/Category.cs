using Core.Persistence.Domain;

namespace Domain.Entities;
public class Category : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } 

    public virtual ICollection<Product> Products { get; set; } = [];
}

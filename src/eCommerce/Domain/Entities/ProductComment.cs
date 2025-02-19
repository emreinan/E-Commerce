using Core.Persistence.Domain;

namespace Domain.Entities;

public class ProductComment : Entity<Guid>
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = null!;
    public byte StarCount { get; set; }
    public bool IsConfirmed { get; set; } = false;

    public virtual Product Product { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}
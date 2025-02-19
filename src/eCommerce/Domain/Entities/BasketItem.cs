using Core.Persistence.Domain;

namespace Domain.Entities;

public class BasketItem : Entity<Guid>
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public virtual Product Product { get; set; } = default!;
    public virtual Basket Basket { get; set; } = default!;
}

using Core.Persistence.Domain;

namespace Domain.Entities;
public class Product : Entity<Guid>
{
    public Guid StoreId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Details { get; set; }
    public int StockAmount { get; set; }
    public bool Enabled { get; set; }

    public virtual Store Store { get; set; } = default!;
    public virtual Category Category { get; set; } = default!;
    public virtual ICollection<ProductImage> ProductImages { get; set; } = default!;
    public virtual ICollection<ProductComment> ProductComments { get; set; } = default!;
    public virtual ICollection<BasketItem> BasketItems { get; set; } = default!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
}

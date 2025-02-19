using Core.Persistence.Domain;

namespace Domain.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public string ProductNameAtOrderTime { get; set; } = default!; // O anki isim
    public decimal ProductPriceAtOrderTime { get; set; } // O anki fiyat
    public int Quantity { get; set; }
    public decimal TotalPrice => ProductPriceAtOrderTime * Quantity;

    public virtual Order Order { get; set; } = default!;
}
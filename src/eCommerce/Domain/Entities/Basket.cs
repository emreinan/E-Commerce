using Core.Persistence.Domain;

namespace Domain.Entities;
public class Basket : Entity<Guid>
{
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public Guid? DiscountId { get; set; }

    public virtual Discount? Discount { get; set; }
    public virtual ICollection<BasketItem> BasketItems { get; set; } = [];
    public virtual User? User { get; set; }
}

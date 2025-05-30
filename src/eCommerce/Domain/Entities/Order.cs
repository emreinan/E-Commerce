using Domain.Enums;
using Core.Persistence.Domain;

namespace Domain.Entities;
public class Order : Entity<Guid>
{
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public Guid ShippingAddressId { get; set; }
    public Guid? DiscountId { get; set; }

    //Guest
    public string? GuestEmail { get; set; } = string.Empty;
    public string? GuestPhoneNumber { get; set; } = string.Empty;

    public string OrderCode { get; set; } = default!;
    public DateTime OrderDate { get; set; }

    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal FinalAmount { get; set; }

    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public bool IsPaid { get; set; } = false;

    public virtual Discount? Discount { get; set; }
    public virtual Address ShippingAddress { get; set; } = default!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
}

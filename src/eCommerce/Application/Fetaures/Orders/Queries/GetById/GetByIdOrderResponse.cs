using Domain.Enums;

namespace Application.Fetaures.Orders.Queries.GetById;

public class GetByIdOrderResponse 
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? GuestId { get; init; }
    public Guid ShippingAddressId { get; set; }
    public Guid? DiscountId { get; set; }
    public string OrderCode { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal FinalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
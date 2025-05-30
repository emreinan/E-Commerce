using Domain.Enums;

namespace Application.Fetaures.Orders.Commands.Create;

public class CreatedOrderResponse 
{
    public Guid Id { get; init; }
    public Guid? UserId { get; init; }
    public string? GuestId { get; init; }
    public Guid ShippingAddressId { get; init; }
    public Guid? DiscountId { get; init; }
    public string OrderCode { get; init; } = default!;
    public DateTime OrderDate { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal ShippingCost { get; init; }
    public decimal FinalAmount { get; init; }
    public OrderStatus Status { get; init; }
    public bool IsPaid { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}
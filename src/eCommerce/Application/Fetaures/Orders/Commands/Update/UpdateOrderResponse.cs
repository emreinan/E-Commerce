using Domain.Enums;

namespace Application.Fetaures.Orders.Commands.Update;

public class UpdateOrderRequest
{
    public required Guid UserId { get; set; }
    public required Guid ShippingAddressId { get; set; }
    public Guid? DiscountId { get; set; }
    public required OrderStatus Status { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }
}
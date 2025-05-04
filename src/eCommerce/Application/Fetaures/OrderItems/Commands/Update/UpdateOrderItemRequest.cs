namespace Application.Fetaures.OrderItems.Commands.Update;

public class UpdateOrderItemRequest
{
    public Guid Id { get; set; }
    public required Guid OrderId { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductNameAtOrderTime { get; set; } = default!;
    public required decimal ProductPriceAtOrderTime { get; set; }
    public required int Quantity { get; set; }
}
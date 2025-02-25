namespace Application.Fetaures.BasketItems.Commands.Update;

public class UpdateBasketItemRequest
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
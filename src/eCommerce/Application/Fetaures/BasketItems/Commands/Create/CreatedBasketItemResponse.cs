namespace Application.Fetaures.BasketItems.Commands.Create;

public class CreatedBasketItemResponse
{
    public Guid Id { get; init; }
    public Guid BasketId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}

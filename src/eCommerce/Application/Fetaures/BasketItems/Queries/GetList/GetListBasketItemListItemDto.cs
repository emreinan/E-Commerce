namespace Application.Fetaures.BasketItems.Queries.GetList;

public class GetListBasketItemListItemDto 
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
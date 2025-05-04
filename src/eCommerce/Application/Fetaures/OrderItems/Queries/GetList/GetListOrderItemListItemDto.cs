namespace Application.Fetaures.OrderItems.Queries.GetList;

public class GetListOrderItemListItemDto 
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductNameAtOrderTime { get; set; } = default!;
    public decimal ProductPriceAtOrderTime { get; set; }
    public int Quantity { get; set; }
}
namespace Application.Fetaures.Products.Queries.GetList;

public class GetListProductListItemDto 
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Details { get; set; }
    public int StockAmount { get; set; }
    public bool Enabled { get; set; }
}
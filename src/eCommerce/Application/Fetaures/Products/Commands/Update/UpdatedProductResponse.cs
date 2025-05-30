namespace Application.Fetaures.Products.Commands.Update;

public class UpdatedProductResponse 
{
    public Guid Id { get; init; }
    public Guid StoreId { get; init; }
    public Guid CategoryId { get; init; }
    public string Name { get; init; } = default!;
    public decimal Price { get; init; }
    public string? Details { get; init; }
    public int StockAmount { get; init; }
    public bool Enabled { get; init; }
}

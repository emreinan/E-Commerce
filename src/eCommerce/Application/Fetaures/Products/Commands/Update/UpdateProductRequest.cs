namespace Application.Fetaures.Products.Commands.Update;

public sealed record UpdateProductRequest(
    Guid StoreId,
    Guid CategoryId,
    string Name,
    decimal Price,
    string? Details,
    int StockAmount,
    bool Enabled
);

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdatedProductImageResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string ImageUrl { get; init; } = default!;
    public bool IsMain { get; init; }
}

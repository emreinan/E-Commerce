namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdatedProductImageResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
}

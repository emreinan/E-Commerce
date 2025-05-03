namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageRequest
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
}
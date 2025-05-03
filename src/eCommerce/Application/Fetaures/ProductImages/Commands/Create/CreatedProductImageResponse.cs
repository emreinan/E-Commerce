namespace Application.Fetaures.ProductImages.Commands.Create;

public class CreatedProductImageResponse 
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
}
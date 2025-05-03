namespace Application.Fetaures.ProductImages.Queries.GetById;

public class GetByIdProductImageResponse 
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
}
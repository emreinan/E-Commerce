
namespace Application.Fetaures.ProductImages.Queries.GetList;

public class GetListProductImageListItemDto 
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = default!;
    public bool IsMain { get; init; }
}
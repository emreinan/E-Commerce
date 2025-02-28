
namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryListItemDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
}
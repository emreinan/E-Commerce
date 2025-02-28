namespace Application.Features.Categories.Queries.GetById;

public class GetByIdCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
}
namespace Application.Fetaures.Categories.Commands.Update;

public class UpdatedCategoryResponse 
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = string.Empty;
}

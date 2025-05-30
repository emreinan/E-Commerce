namespace Application.Fetaures.Categories.Commands.Create;

public class CreatedCategoryResponse
{
    public Guid Id { get; init; } 
    public string Name { get; init; } = default!;
    public string? Description { get; init; } = string.Empty;
}
namespace Application.Fetaures.Categories.Commands.Create;

public class CreatedCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = string.Empty;
}
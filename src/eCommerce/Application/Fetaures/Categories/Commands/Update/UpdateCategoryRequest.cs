namespace Application.Fetaures.Categories.Commands.Update;

public class UpdateCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } 
}
namespace Application.Features.ProductComments.Commands.Create;

public class CreatedProductCommentResponse 
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = default!;
    public byte StarCount { get; set; }
    public bool IsConfirmed { get; set; }
}
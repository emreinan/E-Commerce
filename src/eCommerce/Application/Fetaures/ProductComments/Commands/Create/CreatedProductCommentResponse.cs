namespace Application.Fetaures.ProductComments.Commands.Create;

public class CreatedProductCommentResponse 
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = default!;
    public byte StarCount { get; init; }
    public bool IsConfirmed { get; init; }
}
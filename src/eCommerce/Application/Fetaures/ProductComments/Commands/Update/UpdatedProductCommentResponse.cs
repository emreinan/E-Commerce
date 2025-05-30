namespace Application.Fetaures.ProductComments.Commands.Update;

public class UpdatedProductCommentResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid UserId { get; init; }
    public byte StarCount { get; init; }
    public string Text { get; set; } = default!;
}

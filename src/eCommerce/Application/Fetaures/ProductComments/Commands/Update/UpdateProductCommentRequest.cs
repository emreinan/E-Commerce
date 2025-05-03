namespace Application.Fetaures.ProductComments.Commands.Update;

public class UpdateProductCommentRequest
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = default!;
    public byte StarCount { get; set; }
}
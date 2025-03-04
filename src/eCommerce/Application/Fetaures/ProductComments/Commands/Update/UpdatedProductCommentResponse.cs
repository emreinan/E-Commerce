namespace Application.Features.ProductComments.Commands.Update;

public class UpdatedProductCommentResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = default!;
    public byte StarCount { get; set; }
}

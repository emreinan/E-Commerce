namespace Application.Fetaures.ProductComments.Queries.GetList;

public class GetListProductCommentListItemDto 
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = default!;
    public byte StarCount { get; set; }
    public bool IsConfirmed { get; set; }
}
namespace Application.Fetaures.ProductComments.Commands.Update;

public record UpdateProductCommentRequest(Guid ProductId, Guid UserId, byte StarCount, string Text);

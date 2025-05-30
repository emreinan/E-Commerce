using FluentValidation;

namespace Application.Fetaures.ProductComments.Commands.Delete;

public class DeleteProductCommentCommandValidator : AbstractValidator<DeleteProductCommentCommand>
{
    public DeleteProductCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.ProductComments.Commands.Delete;

public class DeleteProductCommentCommandValidator : AbstractValidator<DeleteProductCommentCommand>
{
    public DeleteProductCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
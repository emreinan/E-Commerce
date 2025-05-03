using FluentValidation;

namespace Application.Fetaures.Stores.Commands.Delete;

public class DeleteStoreCommandValidator : AbstractValidator<DeleteStoreCommand>
{
    public DeleteStoreCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
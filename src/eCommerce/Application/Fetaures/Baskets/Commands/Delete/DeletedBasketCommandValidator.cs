using FluentValidation;

namespace Application.Fetaures.Baskets.Commands.Delete;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Fetaures.BasketItems.Commands.Delete;

public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
{
    public DeleteBasketItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
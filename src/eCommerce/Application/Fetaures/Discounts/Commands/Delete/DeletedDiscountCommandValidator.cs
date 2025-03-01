using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Delete;

public class DeleteDiscountCommandValidator : AbstractValidator<DeleteDiscountCommand>
{
    public DeleteDiscountCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
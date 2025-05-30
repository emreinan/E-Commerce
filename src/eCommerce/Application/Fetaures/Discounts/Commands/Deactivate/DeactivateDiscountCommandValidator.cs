using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Deactivate;

public class DeactivateDiscountCommandValidator : AbstractValidator<DeactivateDiscountCommand>
{
    public DeactivateDiscountCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

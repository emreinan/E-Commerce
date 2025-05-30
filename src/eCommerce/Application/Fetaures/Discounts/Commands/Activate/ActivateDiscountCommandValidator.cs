using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Activate;

public class ActivateDiscountCommandValidator : AbstractValidator<ActivateDiscountCommand>
{
    public ActivateDiscountCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

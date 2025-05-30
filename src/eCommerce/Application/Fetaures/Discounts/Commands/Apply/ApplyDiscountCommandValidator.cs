using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Apply;

public class ApplyDiscountCommandValidator : AbstractValidator<ApplyDiscountCommand>
{
    public ApplyDiscountCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.OrderTotal)
            .GreaterThan(0);
    }
}

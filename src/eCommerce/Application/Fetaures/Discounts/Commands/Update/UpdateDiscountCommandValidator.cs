using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Update;

public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(c => c.Amount)
            .GreaterThan(0)
            .When(c => !c.Percentage.HasValue || c.Percentage == 0)
            .WithMessage("Either Amount or Percentage must be specified.");

        RuleFor(c => c.Percentage)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(c => c.Percentage.HasValue)
            .WithMessage("Percentage must be between 0 and 100.");

        RuleFor(c => c.MinOrderAmount)
            .GreaterThan(0);

        RuleFor(c => c.UsageLimit)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .GreaterThanOrEqualTo(0);

        RuleFor(c => c.EndDate)
            .NotEmpty();
    }
}
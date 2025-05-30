using Domain.Enums;
using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Update;

public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Discount value must be greater than 0.");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .When(x => x.Type == DiscountType.Percentage)
            .WithMessage("Percentage must be between 1 and 100.");

        RuleFor(x => x.MinOrderAmount)
            .GreaterThan(0);

        RuleFor(x => x.UsageLimit)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("End date must be in the future.");
    }
}

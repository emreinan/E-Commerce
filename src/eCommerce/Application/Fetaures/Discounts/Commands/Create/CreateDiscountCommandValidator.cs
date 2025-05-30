using Domain.Enums;
using FluentValidation;

namespace Application.Fetaures.Discounts.Commands.Create;

public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(20)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Code must contain only letters and numbers.");

        RuleFor(c => c.Type)
          .IsInEnum();

        RuleFor(c => c.Value)
            .GreaterThan(0)
            .When(c => c.Type == DiscountType.Amount)
            .WithMessage("Amount discount value must be greater than 0.");

        RuleFor(c => c.Value)
            .InclusiveBetween(1, 100)
            .When(c => c.Type == DiscountType.Percentage)
            .WithMessage("Percentage must be between 1 and 100.");

        RuleFor(c => c.MinOrderAmount)
            .GreaterThan(0);

        RuleFor(c => c.UsageLimit)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .GreaterThanOrEqualTo(0);

        RuleFor(c => c.StartDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Start date cannot be in the past.");

        RuleFor(c => c.EndDate)
            .NotEmpty();

        RuleFor(c => c)
            .Must(c => c.StartDate < c.EndDate)
            .WithMessage("Start date must be less than end date.");
    }
}
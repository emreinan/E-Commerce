using FluentValidation;

namespace Application.Fetaures.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.ShippingAddressId).NotEmpty();
        RuleFor(c => c.ShippingCost).GreaterThanOrEqualTo(0);
        RuleFor(c => c.PaymentMethod).IsInEnum();

         When(c => c.UserId == null, () =>
        {
            RuleFor(c => c.GuestEmail)
                .NotEmpty().WithMessage("Guest email is required when UserId is null.")
                .EmailAddress();
            RuleFor(c => c.GuestPhoneNumber)
                .NotEmpty().WithMessage("Guest phone number is required when UserId is null.");
        });
    }
}
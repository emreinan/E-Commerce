using FluentValidation;

namespace Application.Fetaures.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ShippingAddressId).NotEmpty();
        RuleFor(c => c.ShippingCost).NotEmpty();
        RuleFor(c => c.FinalAmount).NotEmpty();
        RuleFor(c => c.PaymentMethod).NotEmpty();
    }
}
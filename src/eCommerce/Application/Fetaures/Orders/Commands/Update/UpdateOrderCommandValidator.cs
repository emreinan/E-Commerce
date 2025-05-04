using FluentValidation;

namespace Application.Fetaures.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ShippingAddressId).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.PaymentMethod).NotEmpty();
    }
}
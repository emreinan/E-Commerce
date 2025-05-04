using FluentValidation;

namespace Application.Fetaures.OrderItems.Commands.Create;

public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.ProductNameAtOrderTime).NotEmpty();
        RuleFor(c => c.ProductPriceAtOrderTime).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
    }
}
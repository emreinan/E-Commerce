using FluentValidation;

namespace Application.Fetaures.Orders.Commands.Delete;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
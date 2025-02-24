using FluentValidation;

namespace Application.Fetaures.Addresses.Commands.Delete;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
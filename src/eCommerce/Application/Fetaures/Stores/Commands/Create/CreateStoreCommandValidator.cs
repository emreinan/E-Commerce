using FluentValidation;

namespace Application.Fetaures.Stores.Commands.Create;

public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
        RuleFor(c => c.IsVerified).NotEmpty();
    }
}
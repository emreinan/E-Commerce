using FluentValidation;

namespace Application.Fetaures.Stores.Commands.Create;

public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Store name must not exceed 100 characters.");

        RuleFor(c => c.Description)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Phone number must be between 10 and 15 digits and may start with +.");

        RuleFor(c => c.Address)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(c => c.Logo)
            .Must(file => file == null || file.Length < 5 * 1024 * 1024)
            .WithMessage("Logo dosyasý maksimum 5MB olmalýdýr.");
    }

}

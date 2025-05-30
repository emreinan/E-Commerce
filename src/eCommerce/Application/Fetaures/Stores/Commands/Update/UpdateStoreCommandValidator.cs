using FluentValidation;

namespace Application.Fetaures.Stores.Commands.Update;

public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreRequest>
{
    public UpdateStoreCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Email)
          .NotEmpty()
          .EmailAddress();

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Phone number must be between 10 and 15 digits and may start with +.");

        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.IsActive).NotNull();
        RuleFor(c => c.IsVerified).NotNull();

        RuleFor(c => c.Logo)
            .Must(file => file == null || file.Length < 5 * 1024 * 1024)
            .WithMessage("Logo dosyasý maksimum 5MB olmalýdýr.");
    }
}
using FluentValidation;

namespace Application.Fetaures.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressRequest>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(c => c.AddressTitle).NotEmpty().MaximumLength(50);
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required for guests.")
            .When(x => x.UserId == null);
        RuleFor(c => c.Street).NotEmpty().MaximumLength(200);
        RuleFor(c => c.City).NotEmpty().MaximumLength(100);
        RuleFor(c => c.District).NotEmpty().MaximumLength(100);
        RuleFor(c => c.ZipCode).MaximumLength(20);
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(c => c.AddressDetail).NotEmpty().MaximumLength(500);

    }
}
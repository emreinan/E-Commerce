using FluentValidation;

namespace Application.Fetaures.Addresses.Commands.Create;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(c => c.AddressTitle).NotEmpty().MaximumLength(50);
        RuleFor(c => c.FullName).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Street).NotEmpty().MaximumLength(200);
        RuleFor(c => c.City).NotEmpty().MaximumLength(100);
        RuleFor(c => c.District).NotEmpty().MaximumLength(100);
        RuleFor(c => c.ZipCode).MaximumLength(20);
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(c => c.AddressDetail).NotEmpty().MaximumLength(500);
    }
}
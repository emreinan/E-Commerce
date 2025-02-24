using FluentValidation;

namespace Application.Fetaures.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressRequest>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(c => c.AddressTitle).NotEmpty().MaximumLength(50);
        RuleFor(c => c.FullName).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Street).NotEmpty().MaximumLength(200);
        RuleFor(c => c.City).NotEmpty().MaximumLength(100);
        RuleFor(c => c.District).NotEmpty().MaximumLength(100);
        RuleFor(c => c.ZipCode).MaximumLength(20);
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(c => c.AddressDetail).NotEmpty().MaximumLength(500);

        RuleFor(c => c)
            .Must(c => c.UserId.HasValue || !string.IsNullOrEmpty(c.GuestId))
            .WithMessage("UserId veya GuestId'den en az biri belirtilmelidir.");
    }
}
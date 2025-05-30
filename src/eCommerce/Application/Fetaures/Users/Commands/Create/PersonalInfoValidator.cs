using Application.Fetaures.Users.Dtos;
using FluentValidation;

namespace Application.Fetaures.Users.Commands.Create;

public class PersonalInfoValidator : AbstractValidator<PersonalInfoDto?>
{
    public PersonalInfoValidator()
    {
        RuleFor(p => p).NotNull().WithMessage("Personal info is required.");

        When(p => p != null, () =>
        {
            RuleFor(p => p!.TcNo)
                .NotEmpty().WithMessage("TcNo is required.")
                .Length(11).WithMessage("TcNo must be 11 characters.")
                .Matches("^[0-9]+$").WithMessage("TcNo must contain only digits.");

            RuleFor(p => p!.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");

            RuleFor(p => p!.ProfileImageUrl)
                .MaximumLength(200).WithMessage("Profile image URL cannot exceed 200 characters.")
                .When(p => !string.IsNullOrWhiteSpace(p!.ProfileImageUrl));
        });
    }
}

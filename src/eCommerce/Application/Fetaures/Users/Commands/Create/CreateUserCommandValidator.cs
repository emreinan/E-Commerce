using FluentValidation;

namespace Application.Fetaures.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(4);

        When(c => c.PersonalInfo is not null, () =>
        {
            RuleFor(c => c.PersonalInfo)
                .SetValidator(new PersonalInfoValidator()!);
        });

        When(c => c.ProfileImage != null, () =>
        {
            RuleFor(c => c.ProfileImage!.Length)
                .GreaterThan(0).WithMessage("Dosya boş olamaz.");

            RuleFor(c => c.ProfileImage!.ContentType)
                .Must(contentType => new[] { "image/jpeg", "image/png", "image/webp" }.Contains(contentType))
                .WithMessage("Sadece jpeg, png veya webp formatındaki dosyalar destekleniyor.");

            RuleFor(c => c.ProfileImage!.Length)
                .LessThanOrEqualTo(5 * 1024 * 1024) // 5MB
                .WithMessage("Profil resmi 5MB'den büyük olamaz.");
        });
    }
}

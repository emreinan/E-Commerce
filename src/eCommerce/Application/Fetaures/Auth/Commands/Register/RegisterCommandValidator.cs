using FluentValidation;

namespace Application.Fetaures.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<UserForRegisterDto>
{
    public RegisterCommandValidator() 
    {
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
        RuleFor(x=>x.Password).NotEmpty().MinimumLength(4);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.PhoneNumber)
         .NotEmpty()
         .Matches(@"^\+?[0-9]{10,15}$")
         .WithMessage("Telefon numarası geçerli bir formatta olmalı. (Başında + olabilir, 10-15 rakam)");
    }
}

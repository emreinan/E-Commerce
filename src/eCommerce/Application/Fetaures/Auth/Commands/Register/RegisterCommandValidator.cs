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
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}

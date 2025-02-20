using FluentValidation;

namespace Application.Fetaures.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginCommandValidator() 
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Password).NotEmpty().MinimumLength(4);
        }
    }
}

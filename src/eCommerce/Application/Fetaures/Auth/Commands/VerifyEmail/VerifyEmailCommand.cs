using Application.Fetaures.Auth.Rules;
using Application.Services.Repositories;
using MediatR;

namespace Application.Fetaures.Auth.Commands.VerifyEmail;

public class VerifyEmailCommand : IRequest<VerifyEmailResponse>
{
    public string Email { get; set; } = default!;
    public string Code { get; set; } = default!;

    internal class VerifyEmailCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules) : IRequestHandler<VerifyEmailCommand, VerifyEmailResponse>
    {
        public async Task<VerifyEmailResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(u=>u.Email == request.Email);

            authBusinessRules.UserShouldExist(user);

            authBusinessRules.VerificationCodeShouldBeCorrect(user!, request.Code);

            user!.IsActive = true;
            await userRepository.UpdateAsync(user, cancellationToken);

            return new VerifyEmailResponse
            {
                Message = "Email verified successfully"
            };

        }
    }
}

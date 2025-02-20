using Application.Fetaures.Auth.Rules;
using Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Fetaures.Auth.Commands.VerifyEmail;

public class VerifyEmailCommand : IRequest<VerifyEmailResponse>
{
    public string Email { get; set; }
    public string Code { get; set; }

    internal class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public VerifyEmailCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<VerifyEmailResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u=>u.Email == request.Email);

            _authBusinessRules.UserShouldExist(user);

            _authBusinessRules.VerificationCodeShouldBeCorrect(user!, request.Code);

            user!.IsActive = true;
            await _userRepository.UpdateAsync(user);

            return new VerifyEmailResponse
            {
                Message = "Email verified successfully"
            };

        }
    }
}

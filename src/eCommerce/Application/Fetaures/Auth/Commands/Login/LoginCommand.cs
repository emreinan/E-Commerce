using Application.Fetaures.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Fetaures.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoggedResponse>
    {
        public UserForLoginDto Login { get; set; } = default!;
        public string IpAddress { get; set; } = default!;

        internal class LoginCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules) : IRequestHandler<LoginCommand, LoggedResponse>
        {
            public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await userRepository.GetAsync(predicate: u => u.Email == request.Login.Email);

                //business rule 1
                authBusinessRules.UserShouldExist(user);

                //business rule 2
                authBusinessRules.PasswordShouldMatch(request.Login.Password, user!);

                await authService.DeleteOldRefreshTokens(user!.Id);

                var accessToken = authService.CreateAccessToken(user!);
                var refreshToken = await authService.CreateRefreshTokenAsync(user!, request.IpAddress);

                return new LoggedResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                };
            }
        }
    }
}

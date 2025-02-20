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
        public UserForLoginDto Login { get; set; }
        public string IpAddress { get; set; }

        internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(predicate: u => u.Email == request.Login.Email);

                //business rule 1
                _authBusinessRules.UserShouldExist(user);

                //business rule 2
                _authBusinessRules.PasswordShouldMatch(request.Login.Password, user!);

                await _authService.DeleteOldRefreshTokens(user!.Id);

                var accessToken = _authService.CreateAccessToken(user!);
                var refreshToken = await _authService.CreateRefreshTokenAsync(user!, request.IpAddress);

                return new LoggedResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                };
            }
        }
    }
}

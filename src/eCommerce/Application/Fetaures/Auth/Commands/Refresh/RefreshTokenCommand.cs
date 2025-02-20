using Application.Fetaures.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Auth.Commands.Refresh.RefreshTokenCommand;

namespace Application.Features.Auth.Commands.Refresh;

public partial class RefreshTokenCommand : IRequest<RefreshedTokenResponse>
{
    public string Token { get; set; }
    public string IpAddress { get; set; }

    internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public RefreshTokenCommandHandler(IAuthService authService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RefreshedTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(t => t.Token == request.Token);

            //1. check if the token  exists
            _authBusinessRules.RefreshTokenShouldExist(refreshToken);

            //2. check if the token is expired

            _authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

            //3. check if ip address match
            _authBusinessRules.IpAddressShouldMatch(refreshToken!, request.IpAddress);

            //4. check if user exists
            var user = await _userRepository.GetAsync(u => u.Id == refreshToken!.UserId);
            _authBusinessRules.UserShouldExist(user);

            await _authService.DeleteOldRefreshTokens(user!.Id);

            await _authService.RotateRefreshToken(user!, refreshToken!, request.IpAddress);
            var accessToken = _authService.CreateAccessToken(user!);

            return new RefreshedTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken!.Token
            };

        }
    }
}

using Application.Fetaures.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using MediatR;

using static Application.Fetaures.Auth.Commands.Refresh.RefreshTokenCommand;

namespace Application.Fetaures.Auth.Commands.Refresh;

public partial class RefreshTokenCommand : IRequest<RefreshedTokenResponse>
{
    public string Token { get; set; } = default!;
    public string IpAddress { get; set; } = default!;

    internal class RefreshTokenCommandHandler(IAuthService authService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, AuthBusinessRules authBusinessRules) : IRequestHandler<RefreshTokenCommand, RefreshedTokenResponse>
    {
        public async Task<RefreshedTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await refreshTokenRepository.GetAsync(t => t.Token == request.Token);

            //1. check if the token  exists
            authBusinessRules.RefreshTokenShouldExist(refreshToken);

            //2. check if the token is expired
            authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);

            //3. check if ip address match
            authBusinessRules.IpAddressShouldMatch(refreshToken!, request.IpAddress);

            //4. check if user exists
            var user = await userRepository.GetAsync(u => u.Id == refreshToken!.UserId, cancellationToken: cancellationToken);
            authBusinessRules.UserShouldExist(user);

            await authService.DeleteOldRefreshTokens(user!.Id);

            await authService.RotateRefreshToken(user!, refreshToken!, request.IpAddress);
            var accessToken = authService.CreateAccessToken(user!);

            return new RefreshedTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken!.Token
            };

        }
    }
}

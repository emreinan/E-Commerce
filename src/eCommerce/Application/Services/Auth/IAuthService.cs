using Domain.Entities;

namespace Application.Services.Auth;

public interface IAuthService
{
    string CreateAccessToken(User user);
    Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress);
    Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress);
    Task DeleteOldRefreshTokens(Guid userId);
}

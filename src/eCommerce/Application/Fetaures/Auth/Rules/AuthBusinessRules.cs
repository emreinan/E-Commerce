using Application.Fetaures.Auth.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Security;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Fetaures.Auth.Rules;

public class AuthBusinessRules(IUserRepository userRepository, IRoleRepository roleRepository)
{
    public void UserShouldExist(User? user)
    {
        if (user is null)
            throw new BusinessException(ErrorMessages.UserNotFound);
    }

    public void PasswordShouldMatch(string password, User user)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(ErrorMessages.InvalidPassword);
    }

    public async Task EmailShouldBeUnique(string email)
    {
        if (await userRepository.AnyAsync(u => u.Email == email))
            throw new BusinessException(ErrorMessages.EmailInUse);
    }

    public void RefreshTokenShouldExist(RefreshToken? refreshToken)
    {
        if (refreshToken is null)
            throw new BusinessException(ErrorMessages.RefreshTokenNotFound);
    }

    public void RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate is not null || refreshToken.ExpiresAt <= DateTime.UtcNow)
            throw new BusinessException(ErrorMessages.InvalidRefreshToken);
    }

    public void IpAddressShouldMatch(RefreshToken refreshToken, string ipAddress)
    {
        if (refreshToken.CreatedByIp != ipAddress)
            throw new BusinessException(ErrorMessages.IpDoesNotMatch);
    }

    public void VerificationCodeShouldBeCorrect(User user, string code)
    {
        if (user.VerificationCode is null || user.VerificationCode != code)
            throw new BusinessException(ErrorMessages.InvalidVerificationCode);
    }
    public async Task<Role> UserRoleIfNotFoundCreate(CancellationToken cancellationToken)
    {
        var userRole = await roleRepository.GetAsync(r => r.Name == "User", cancellationToken: cancellationToken);

        if (userRole is null)
        {
            userRole = new Role
            {
                Name = "User"
            };
            await roleRepository.AddAsync(userRole, cancellationToken);
        }
        return userRole;
    }
}
using Application.Fetaures.Users.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Security;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Users.Rules;

public class UserBusinessRules(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{

    public void UserShouldBeExistsWhenSelected(User? user)
    {
        if (user is null)
            throw new BusinessException(UsersMessages.UserDontExists);
    }

    public async Task UserIdShouldBeExistsWhenSelected(Guid id)
    {
        bool doesExist = await userRepository.AnyAsync(predicate: u => u.Id == id);
        if (doesExist)
            throw new BusinessException(UsersMessages.UserDontExists);
    }

    public void UserPasswordShouldBeMatched(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(UsersMessages.PasswordDontMatch);
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await userRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            throw new BusinessException(UsersMessages.UserMailAlreadyExists);
    }

    public async Task UserEmailShouldNotExistsWhenUpdate(Guid id, string email)
    {
        bool doesExists = await userRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
        if (doesExists)
            throw new BusinessException(UsersMessages.UserMailAlreadyExists);
    }
}

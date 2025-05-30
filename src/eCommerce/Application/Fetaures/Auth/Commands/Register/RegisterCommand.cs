using Application.Fetaures.Auth.Events;
using Application.Fetaures.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Security;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto Register { get; set; } = default!;
    public string IpAddress { get; set; } = default!;

    internal class RegisterCommandHandler(IUserRepository userRepository,IAuthService authService, AuthBusinessRules authBusinessRules, IMediator mediator) : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await authBusinessRules.EmailShouldBeUnique(request.Register.Email);

            HashingHelper.CreatePasswordHash(request.Register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userRole = await authBusinessRules.UserRoleIfNotFoundCreate(cancellationToken);

            User newUser = new()
            {
                Email = request.Register.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.Register.FirstName,
                LastName = request.Register.LastName,
                PhoneNumber = request.Register.PhoneNumber,
                IsActive = false,
                UserRoles =
                [
                    new UserRole
                    {
                        RoleId = userRole.Id,
                        Role = userRole,
                        CreatedDate = DateTime.UtcNow,
                    }
                ],

            };

            await userRepository.AddAsync(newUser, cancellationToken);

            var accessToken = authService.CreateAccessToken(newUser);
            var refreshToken = await authService.CreateRefreshTokenAsync(newUser, request.IpAddress);

            await mediator.Publish(new SendEmailVerificationEvent
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                Name = newUser.FirstName
            }, cancellationToken);

            return new RegisteredResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}

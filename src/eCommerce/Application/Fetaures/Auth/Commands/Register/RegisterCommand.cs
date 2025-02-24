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

    internal class RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IMediator mediator) : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await authBusinessRules.EmailShouldBeUnique(request.Register.Email);

            HashingHelper.CreatePasswordHash(request.Register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new()
            {
                Email = request.Register.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.Register.FirstName,
                LastName = request.Register.LastName,
                PhoneNumber = request.Register.PhoneNumber,
                IsActive = false,
                UserRoles = [new() {Role = new Role {Name = "User"}}]
            };

            await userRepository.AddAsync(newUser);

            var accessToken = authService.CreateAccessToken(newUser);
            var refreshToken = await authService.CreateRefreshTokenAsync(newUser, request.IpAddress);

            await mediator.Publish(new SendEmailVerificationEvent
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                Name = newUser.FirstName
            });

            return new RegisteredResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}

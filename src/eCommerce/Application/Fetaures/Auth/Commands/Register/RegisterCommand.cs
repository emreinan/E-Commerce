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
    public UserForRegisterDto Register { get; set; }
    public string IpAddress { get; set; }

    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IMediator mediator)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mediator = mediator;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.EmailShouldBeUnique(request.Register.Email);

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

            await _userRepository.AddAsync(newUser);

            var accessToken = _authService.CreateAccessToken(newUser);
            var refreshToken = await _authService.CreateRefreshTokenAsync(newUser, request.IpAddress);

            await _mediator.Publish(new SendEmailVerificationEvent
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

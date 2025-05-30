using Application.Fetaures.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Core.Application.Security;
using Application.Fetaures.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Application.Services.File;

namespace Application.Fetaures.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public PersonalInfoDto? PersonalInfo { get; set; }
    public IFormFile? ProfileImage { get; set; }

    public class CreateUserCommandHandler(IUserRepository userRepository, IFileService fileService, IMapper mapper, UserBusinessRules userBusinessRules) : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Email);

            if (request.ProfileImage is not null)
            {
                var uploadedFile = await fileService.UploadFileAsync(request.ProfileImage);

                request.PersonalInfo ??= new PersonalInfoDto();

                request.PersonalInfo.ProfileImageUrl =uploadedFile.Url;
            }

            User user = mapper.Map<User>(request);

            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            User createdUser = await userRepository.AddAsync(user, cancellationToken);

            CreatedUserResponse response = mapper.Map<CreatedUserResponse>(createdUser);
            return response;
        }
    }
}

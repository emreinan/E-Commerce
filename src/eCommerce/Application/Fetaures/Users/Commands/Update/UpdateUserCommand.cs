using Application.Fetaures.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Core.Application.Security;
using Application.Services.File;

namespace Application.Fetaures.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>
{
    public Guid Id { get; set; }
    public UpdateUserRequest Request { get; set; } = default!;

    public class UpdateUserCommandHandler(IUserRepository userRepository,
                                          IMapper mapper,
                                          IFileService fileService,
                                          UserBusinessRules userBusinessRules) : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken);

            userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);

            if (request.Request.ProfileImage is not null)
            {
                var fileResponse = await fileService.UploadFileAsync(request.Request.ProfileImage);
                user.PersonalInfo ??= new(); 
                user.PersonalInfo.ProfileImageUrl = fileResponse.Url;
            }

            user = mapper.Map(request.Request, user);

            HashingHelper.CreatePasswordHash(
                request.Request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user!.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await userRepository.UpdateAsync(user, cancellationToken);

            UpdatedUserResponse response = mapper.Map<UpdatedUserResponse>(user);
            return response;
        }
    }
}

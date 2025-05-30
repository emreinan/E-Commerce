using Application.Fetaures.Users.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Users.Commands.Delete;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }


    public class DeleteUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            userBusinessRules.UserShouldBeExistsWhenSelected(user);

            await userRepository.DeleteAsync(user!, cancellationToken: cancellationToken);
        }
    }
}

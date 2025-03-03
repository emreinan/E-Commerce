using Application.Fetaures.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
{
    public Guid Id { get; set; }
    public class GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules) : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.GetAsync(
                predicate: b => b.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = mapper.Map<GetByIdUserResponse>(user);
            return response;
        }
    }
}

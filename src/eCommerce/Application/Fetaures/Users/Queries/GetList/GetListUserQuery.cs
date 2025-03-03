using Application.Fetaures.Users.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Users.Queries.GetList;

public class GetListUserQuery : IRequest<IEnumerable<GetListUserListItemDto>>
{
    public class GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetListUserQuery, IEnumerable<GetListUserListItemDto>>
    {
        public async Task<IEnumerable<GetListUserListItemDto>> Handle(
            GetListUserQuery request,
            CancellationToken cancellationToken
        )
        {
            IEnumerable<User> users = await userRepository.GetListAsync(
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            IEnumerable<GetListUserListItemDto> response = mapper.Map<IEnumerable<GetListUserListItemDto>>(users);
            return response;
        }
    }
}

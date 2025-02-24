using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Queries.GetList;

public class GetListAddressQuery : IRequest<ICollection<GetListAddressListItemDto>>
{
    public class GetListAddressQueryHandler(IAddressRepository addressRepository, IMapper mapper) : IRequestHandler<GetListAddressQuery, ICollection<GetListAddressListItemDto>>
    {
        public async Task<ICollection<GetListAddressListItemDto>> Handle(GetListAddressQuery request, CancellationToken cancellationToken)
        {
            var addresses = await addressRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<ICollection<GetListAddressListItemDto>>(addresses);
            return response;
        }
    }
}
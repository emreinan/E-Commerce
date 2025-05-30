using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Stores.Queries.GetList;

public class GetListStoreQuery : IRequest<IEnumerable<GetListStoreListItemDto>>
{

    public class GetListStoreQueryHandler(IStoreRepository storeRepository, IMapper mapper) : IRequestHandler<GetListStoreQuery, IEnumerable<GetListStoreListItemDto>>
    {
        public async Task<IEnumerable<GetListStoreListItemDto>> Handle(GetListStoreQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Store> stores = (IEnumerable<Store>)await storeRepository.GetListAsync(
                cancellationToken: cancellationToken
            );

            var response = mapper.Map<IEnumerable<GetListStoreListItemDto>>(stores);
            return response;
        }
    }
}
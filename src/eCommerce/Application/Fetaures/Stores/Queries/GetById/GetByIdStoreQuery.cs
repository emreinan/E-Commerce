using Application.Fetaures.Stores.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Stores.Queries.GetById;

public class GetByIdStoreQuery : IRequest<GetByIdStoreResponse>
{
    public Guid Id { get; set; }

    public class GetByIdStoreQueryHandler(IMapper mapper, IStoreRepository storeRepository, StoreBusinessRules storeBusinessRules) : IRequestHandler<GetByIdStoreQuery, GetByIdStoreResponse>
    {
        public async Task<GetByIdStoreResponse> Handle(GetByIdStoreQuery request, CancellationToken cancellationToken)
        {
            Store? store = await storeRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            storeBusinessRules.StoreShouldExistWhenSelected(store);

            GetByIdStoreResponse response = mapper.Map<GetByIdStoreResponse>(store);
            return response;
        }
    }
}
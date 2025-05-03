using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Stores.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Stores.Commands.Update;

public class UpdateStoreCommand : IRequest<UpdatedStoreResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required UpdateStoreRequest Request { get; set; } 

    public class UpdateStoreCommandHandler(IMapper mapper, IStoreRepository storeRepository,
                                     StoreBusinessRules storeBusinessRules) : IRequestHandler<UpdateStoreCommand, UpdatedStoreResponse>
    {
        public async Task<UpdatedStoreResponse> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
        {
            Store? store = await storeRepository.GetAsync(predicate: s => s.Id == command.Id, cancellationToken: cancellationToken);
            storeBusinessRules.StoreShouldExistWhenSelected(store);
            store = mapper.Map(command, store);

            await storeRepository.UpdateAsync(store!);

            UpdatedStoreResponse response = mapper.Map<UpdatedStoreResponse>(store);
            return response;
        }
    }
}
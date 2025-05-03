using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Stores.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Stores.Commands.Delete;

public class DeleteStoreCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteStoreCommandHandler(IStoreRepository storeRepository, StoreBusinessRules storeBusinessRules) : IRequestHandler<DeleteStoreCommand>
    {
        public async Task Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            Store? store = await storeRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            storeBusinessRules.StoreShouldExistWhenSelected(store);

            await storeRepository.DeleteAsync(store!);
        }
    }
}

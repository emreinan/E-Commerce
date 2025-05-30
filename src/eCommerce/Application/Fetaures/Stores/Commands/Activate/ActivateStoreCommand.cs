using Application.Fetaures.Stores.Rules;
using Application.Services.Repositories;
using MediatR;

public class ActivateStoreCommand : IRequest
{
    public required Guid StoreId { get; set; }

    public class ActivateStoreCommandHandler(
        IStoreRepository storeRepository,
        StoreBusinessRules storeBusinessRules) : IRequestHandler<ActivateStoreCommand>
    {
        public async Task Handle(ActivateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeRepository.GetAsync(s => s.Id == request.StoreId, cancellationToken: cancellationToken);

            storeBusinessRules.StoreShouldExistWhenSelected(store);

            storeBusinessRules.StoreShouldBeVerifiedBeforeActivation(store!);

            storeBusinessRules.StoreShouldNotBeAlreadyActive(store!);

            store!.IsActive = true;
            await storeRepository.UpdateAsync(store, cancellationToken);
        }
    }
}

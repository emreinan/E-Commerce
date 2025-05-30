using Application.Fetaures.Stores.Rules;
using Application.Services.Repositories;
using MediatR;

public class VerifyStoreCommand : IRequest
{
    public required Guid StoreId { get; set; }
    public required string Email { get; set; }

    public class VerifyStoreCommandHandler(
        IStoreRepository storeRepository,
        StoreBusinessRules storeBusinessRules,
        IMediator mediator) : IRequestHandler<VerifyStoreCommand>
    {
        public async Task Handle(VerifyStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await storeRepository.GetAsync(
                s => s.Id == request.StoreId && s.Email == request.Email,
                cancellationToken: cancellationToken
            );

            storeBusinessRules.StoreShouldExistWhenSelected(store);

            storeBusinessRules.StoreShouldNotBeAlreadyVerified(store!);

            store!.IsVerified = true;
            await storeRepository.UpdateAsync(store, cancellationToken);

            await mediator.Publish(new SendAdminStoreNotificationEvent
            {
                StoreId = store.Id,
                StoreName = store.Name,
                OwnerEmail = store.Email
            }, cancellationToken);
        }
    }
}


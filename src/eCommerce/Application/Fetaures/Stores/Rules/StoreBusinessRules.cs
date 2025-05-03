using Application.Fetaures.Stores.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Stores.Rules;

public class StoreBusinessRules(IStoreRepository storeRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void StoreShouldExistWhenSelected(Store? store)
    {
        if (store is null)
            throw new BusinessException(StoresBusinessMessages.StoreNotExists);
    }

    public async Task StoreIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Store? store = await storeRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        StoreShouldExistWhenSelected(store);
    }
}
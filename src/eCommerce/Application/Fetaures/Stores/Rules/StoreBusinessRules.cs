using Application.Fetaures.Stores.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

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
    public async Task StoreNameCannotBeDuplicated(string name, CancellationToken cancellationToken)
    {
        bool exists = await storeRepository.AnyAsync(s => s.Name.ToLower() == name.ToLower(), cancellationToken: cancellationToken);
        if (exists)
            throw new BusinessException(StoresBusinessMessages.StoreNameAlreadyExists);
    }
    public async Task StoreEmailCannotBeDuplicated(string email, CancellationToken cancellationToken)
    {
        bool exists = await storeRepository.AnyAsync(s => s.Email.ToLower() == email.ToLower(), cancellationToken: cancellationToken);
        if (exists)
            throw new BusinessException(StoresBusinessMessages.StoreEmailAlreadyExists);
    }
    public void StoreShouldNotBeAlreadyVerified(Store store)
    {
        if (store.IsVerified)
            throw new BusinessException(StoresBusinessMessages.StoreAlreadyVerified);
    }

    public void StoreShouldBeVerifiedBeforeActivation(Store store)
    {
        if (!store.IsVerified)
            throw new BusinessException(StoresBusinessMessages.StoreMustBeVerifiedBeforeActivation);
    }

    public void StoreShouldNotBeAlreadyActive(Store store)
    {
        if (store.IsActive)
            throw new BusinessException(StoresBusinessMessages.StoreAlreadyActive);
    }
}
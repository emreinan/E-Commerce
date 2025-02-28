using Application.Features.Baskets.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Baskets.Rules;

public class BasketBusinessRules(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void BasketShouldExistWhenSelected(Basket? basket)
    {
        if (basket is null)
            throw new BusinessException(BasketsBusinessMessages.BasketNotExists);
    }

    public async Task BasketIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Basket? basket = await basketRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        BasketShouldExistWhenSelected(basket);
    }
}
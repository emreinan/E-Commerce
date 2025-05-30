using Application.Fetaures.Baskets.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Baskets.Rules;

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
    public void BasketShouldHaveItems(Basket basket)
    {
        if (basket.BasketItems is null || basket.BasketItems.Count == 0)
            throw new BusinessException(BasketsBusinessMessages.BasketIsEmpty);
    }

    public async Task<Basket?> GetOrCreateBasketAsync(Guid? userId, string? guestId)
    {
        var basket = await basketRepository.GetAsync(x =>
            x.IsActive && (
                (userId.HasValue && x.UserId == userId) ||
                (!string.IsNullOrWhiteSpace(guestId) && x.GuestId == guestId)
            )
        );
        return basket;
    }

}
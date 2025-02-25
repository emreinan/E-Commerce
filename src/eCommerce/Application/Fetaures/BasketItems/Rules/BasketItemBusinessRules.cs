using Application.Services.Repositories;
using Core.Application.Rules;
using Domain.Entities;
using Application.Fetaures.BasketItems.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Fetaures.BasketItems.Commands.Create;
using AutoMapper;

namespace Application.Fetaures.BasketItems.Rules;

public class BasketItemBusinessRules(IBasketItemRepository basketItemRepository,
                                     IProductRepository productRepository,
                                     IBasketRepository basketRepository,
                                     IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void BasketItemShouldExist(BasketItem? basketItem)
    {
        if (basketItem is null)
            throw new BusinessException(BasketItemsBusinessMessages.BasketItemNotExists);
    }

    public async Task BasketItemIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BasketItem? basketItem = await basketItemRepository.GetAsync(
            predicate: bi => bi.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        BasketItemShouldExist(basketItem);
    }

    public async Task CheckProductAndStockAsync(Guid productId, int requestedQuantity)
    {
        Product? product = await productRepository.GetAsync(p => p.Id == productId);
        if (product is null)
            throw new BusinessException("Product does not exist.");
        if (product.StockAmount < requestedQuantity)
            throw new BusinessException("Insufficient stock for the product.");
    }
    public async Task<Basket> GetOrCreateBasketAsync(Guid? userId, string? guestId, CancellationToken cancellationToken)
    {
        Basket? basket = await basketRepository.GetAsync(
               b => (b.UserId == userId || b.GuestId == guestId), cancellationToken: cancellationToken);

        if (basket is null)
        {
            basket = new Basket
            {
                UserId = userId,
                GuestId = guestId
            };
            await basketRepository.AddAsync(basket, cancellationToken);
        }
        return basket;
    }
    public async Task ReduceOrDeleteBasketItemAsync(BasketItem basketItem, CancellationToken cancellationToken)
    {
        if (basketItem.Quantity == 0)
            await basketItemRepository.DeleteAsync(basketItem,cancellationToken: cancellationToken);
    }
    public async Task CheckAndDeleteBasketIfEmptyAsync(Guid basketId, CancellationToken cancellationToken)
    {
        bool hasItems = await basketItemRepository.AnyAsync(bi => bi.BasketId == basketId, cancellationToken: cancellationToken);
        if (!hasItems)
        {
            Basket? basket = await basketRepository.GetAsync(b => b.Id == basketId, cancellationToken: cancellationToken);
            if (basket is not null)
            {
                await basketRepository.DeleteAsync(basket, cancellationToken: cancellationToken);
            }
        }
    }
}
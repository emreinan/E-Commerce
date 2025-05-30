using Application.Fetaures.Orders.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Orders.Rules;

public class OrderBusinessRules(IOrderRepository orderRepository,
    IDiscountRepository discountRepository,
    IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void OrderShouldExistWhenSelected(Order? order)
    {
        if (order is null)
            throw new BusinessException(OrdersBusinessMessages.OrderNotExists);
    }

    public async Task OrderIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository.GetAsync(
            predicate: o => o.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        OrderShouldExistWhenSelected(order);
    }
    public async Task DiscountShouldBeUsable(Guid? discountId, CancellationToken cancellationToken)
    {
        if (discountId is null) return;

        Discount? discount = await discountRepository.GetAsync(
            d => d.Id == discountId,
            cancellationToken: cancellationToken);

        if (discount is null || !discount.IsUsable)
            throw new BusinessException(OrdersBusinessMessages.DiscountIsNotUsable);
    }

}
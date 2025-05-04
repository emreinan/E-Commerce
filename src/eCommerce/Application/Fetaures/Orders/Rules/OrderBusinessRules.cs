using Application.Fetaures.Orders.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Orders.Rules;

public class OrderBusinessRules(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
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
}
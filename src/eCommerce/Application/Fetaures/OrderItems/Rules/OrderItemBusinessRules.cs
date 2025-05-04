using Application.Fetaures.OrderItems.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.OrderItems.Rules;

public class OrderItemBusinessRules(IOrderItemRepository orderItemRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void OrderItemShouldExistWhenSelected(OrderItem? orderItem)
    {
        if (orderItem is null)
            throw new BusinessException(OrderItemsBusinessMessages.OrderItemNotExists);
    }

    public async Task OrderItemIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        OrderItem? orderItem = await orderItemRepository.GetAsync(
            predicate: oi => oi.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        OrderItemShouldExistWhenSelected(orderItem);
    }
}
using Application.Fetaures.OrderItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.OrderItems.Commands.Update;

public class UpdateOrderItemCommand : IRequest<UpdatedOrderItemResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required UpdateOrderItemRequest Request { get; set; }

    public class UpdateOrderItemCommandHandler(IMapper mapper, IOrderItemRepository orderItemRepository,
                                     OrderItemBusinessRules orderItemBusinessRules) : IRequestHandler<UpdateOrderItemCommand, UpdatedOrderItemResponse>
    {
        public async Task<UpdatedOrderItemResponse> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            OrderItem? orderItem = await orderItemRepository.GetAsync(predicate: oi => oi.Id == request.Id, cancellationToken: cancellationToken);
            orderItemBusinessRules.OrderItemShouldExistWhenSelected(orderItem);
            orderItem = mapper.Map(request.Request, orderItem);

            await orderItemRepository.UpdateAsync(orderItem!, cancellationToken);

            UpdatedOrderItemResponse response = mapper.Map<UpdatedOrderItemResponse>(orderItem);
            return response;
        }
    }
}
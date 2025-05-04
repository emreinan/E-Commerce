using Application.Fetaures.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<UpdatedOrderResponse>, ITransactionalRequest
{
    public required Guid Id { get; set; }
    public required UpdateOrderRequest Request { get; set; }

    public class UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,
                                     OrderBusinessRules orderBusinessRules) : IRequestHandler<UpdateOrderCommand, UpdatedOrderResponse>
    {
        public async Task<UpdatedOrderResponse> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            Order? order = await orderRepository.GetAsync(predicate: o => o.Id == command.Id, cancellationToken: cancellationToken);
            orderBusinessRules.OrderShouldExistWhenSelected(order);
            order = mapper.Map(command.Request, order);

            await orderRepository.UpdateAsync(order!, cancellationToken);

            UpdatedOrderResponse response = mapper.Map<UpdatedOrderResponse>(order);
            return response;
        }
    }
}
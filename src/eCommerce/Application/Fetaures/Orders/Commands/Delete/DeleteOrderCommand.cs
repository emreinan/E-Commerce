using Application.Fetaures.Orders.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Orders.Commands.Delete;

public class DeleteOrderCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteOrderCommandHandler(IOrderRepository orderRepository,
                                     OrderBusinessRules orderBusinessRules) : IRequestHandler<DeleteOrderCommand>
    {
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = await orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            orderBusinessRules.OrderShouldExistWhenSelected(order);

            await orderRepository.DeleteAsync(order!);
        }
    }
}
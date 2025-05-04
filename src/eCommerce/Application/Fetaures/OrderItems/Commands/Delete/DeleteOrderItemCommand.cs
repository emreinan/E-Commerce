using Application.Fetaures.OrderItems.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.OrderItems.Commands.Delete;

public class DeleteOrderItemCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository,
                                     OrderItemBusinessRules orderItemBusinessRules) : IRequestHandler<DeleteOrderItemCommand>
    {
        public async Task Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            OrderItem? orderItem = await orderItemRepository.GetAsync(predicate: oi => oi.Id == request.Id, cancellationToken: cancellationToken);
            orderItemBusinessRules.OrderItemShouldExistWhenSelected(orderItem);

            await orderItemRepository.DeleteAsync(orderItem!, cancellationToken);
        }
    }
}
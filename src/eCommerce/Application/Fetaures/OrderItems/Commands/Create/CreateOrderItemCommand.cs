using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.OrderItems.Commands.Create;

public class CreateOrderItemCommand : IRequest<CreatedOrderItemResponse>, ITransactionalRequest
{
    public required Guid OrderId { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductNameAtOrderTime { get; set; }
    public required decimal ProductPriceAtOrderTime { get; set; }
    public required int Quantity { get; set; }

    public class CreateOrderItemCommandHandler(IMapper mapper, IOrderItemRepository orderItemRepository) : IRequestHandler<CreateOrderItemCommand, CreatedOrderItemResponse>
    {
        public async Task<CreatedOrderItemResponse> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(request);

            await orderItemRepository.AddAsync(orderItem);

            CreatedOrderItemResponse response = mapper.Map<CreatedOrderItemResponse>(orderItem);
            return response;
        }
    }
}
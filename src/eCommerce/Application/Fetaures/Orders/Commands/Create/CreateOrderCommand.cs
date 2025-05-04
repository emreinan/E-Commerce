using Application.Fetaures.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Fetaures.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<CreatedOrderResponse>, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public required Guid ShippingAddressId { get; set; }
    public Guid? DiscountId { get; set; }
    public required decimal ShippingCost { get; set; }
    public required decimal FinalAmount { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }

    public class CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository) : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = mapper.Map<Order>(request);

            await orderRepository.AddAsync(order);

            CreatedOrderResponse response = mapper.Map<CreatedOrderResponse>(order);
            return response;
        }
    }
}
using Application.Fetaures.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Orders.Queries.GetById;

public class GetByIdOrderQuery : IRequest<GetByIdOrderResponse>
{
    public Guid Id { get; set; }

    public class GetByIdOrderQueryHandler(IMapper mapper, IOrderRepository orderRepository, OrderBusinessRules orderBusinessRules) : IRequestHandler<GetByIdOrderQuery, GetByIdOrderResponse>
    {
        public async Task<GetByIdOrderResponse> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            Order? order = await orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            orderBusinessRules.OrderShouldExistWhenSelected(order);

            GetByIdOrderResponse response = mapper.Map<GetByIdOrderResponse>(order);
            return response;
        }
    }
}
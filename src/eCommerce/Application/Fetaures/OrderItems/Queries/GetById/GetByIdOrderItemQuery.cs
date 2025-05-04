using Application.Fetaures.OrderItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.OrderItems.Queries.GetById;

public class GetByIdOrderItemQuery : IRequest<GetByIdOrderItemResponse>
{
    public Guid Id { get; set; }

    public class GetByIdOrderItemQueryHandler(IMapper mapper, IOrderItemRepository orderItemRepository, OrderItemBusinessRules orderItemBusinessRules) : IRequestHandler<GetByIdOrderItemQuery, GetByIdOrderItemResponse>
    {
        public async Task<GetByIdOrderItemResponse> Handle(GetByIdOrderItemQuery request, CancellationToken cancellationToken)
        {
            OrderItem? orderItem = await orderItemRepository.GetAsync(predicate: oi => oi.Id == request.Id, cancellationToken: cancellationToken);
            orderItemBusinessRules.OrderItemShouldExistWhenSelected(orderItem);

            GetByIdOrderItemResponse response = mapper.Map<GetByIdOrderItemResponse>(orderItem);
            return response;
        }
    }
}
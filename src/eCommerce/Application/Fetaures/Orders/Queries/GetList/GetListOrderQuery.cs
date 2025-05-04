using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Orders.Queries.GetList;

public class GetListOrderQuery : IRequest<IEnumerable<GetListOrderListItemDto>>
{

    public class GetListOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetListOrderQuery, IEnumerable<GetListOrderListItemDto>>
    {
        public async Task<IEnumerable<GetListOrderListItemDto>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Order> orders = await orderRepository.GetListAsync(
                cancellationToken: cancellationToken
            );

            IEnumerable<GetListOrderListItemDto> response = mapper.Map<IEnumerable<GetListOrderListItemDto>>(orders);
            return response;
        }
    }
}
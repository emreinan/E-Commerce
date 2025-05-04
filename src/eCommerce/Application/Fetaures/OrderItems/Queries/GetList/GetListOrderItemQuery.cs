using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.OrderItems.Queries.GetList;

public class GetListOrderItemQuery : IRequest<IEnumerable<GetListOrderItemListItemDto>>
{

    public class GetListOrderItemQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper) : IRequestHandler<GetListOrderItemQuery, IEnumerable<GetListOrderItemListItemDto>>
    {
        public async Task<IEnumerable<GetListOrderItemListItemDto>> Handle(GetListOrderItemQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<OrderItem> orderItems = await orderItemRepository.GetListAsync(
                cancellationToken: cancellationToken
            );

            IEnumerable<GetListOrderItemListItemDto> response = mapper.Map<IEnumerable<GetListOrderItemListItemDto>>(orderItems);
            return response;
        }
    }
}
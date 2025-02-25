using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.BasketItems.Queries.GetList;

public class GetListBasketItemQuery : IRequest<ICollection<GetListBasketItemListItemDto>>
{

    public class GetListBasketItemQueryHandler(IBasketItemRepository basketItemRepository, IMapper mapper) : IRequestHandler<GetListBasketItemQuery, ICollection<GetListBasketItemListItemDto>>
    {
        public async Task<ICollection<GetListBasketItemListItemDto>> Handle(GetListBasketItemQuery request, CancellationToken cancellationToken)
        {
            var basketItems = await basketItemRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<ICollection<GetListBasketItemListItemDto>>(basketItems);
            return response;
        }
    }
}
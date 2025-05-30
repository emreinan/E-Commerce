using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Baskets.Queries.GetList;

public class GetListBasketQuery : IRequest<IEnumerable<GetListBasketListItemDto>>
{

    public class GetListBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper) : IRequestHandler<GetListBasketQuery, IEnumerable<GetListBasketListItemDto>>
    {
        public async Task<IEnumerable<GetListBasketListItemDto>> Handle(GetListBasketQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Basket> baskets = await basketRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<IEnumerable<GetListBasketListItemDto>>(baskets);
            return response;
        }
    }
}
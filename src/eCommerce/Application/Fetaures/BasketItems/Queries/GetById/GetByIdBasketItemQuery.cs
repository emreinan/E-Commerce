using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.BasketItems.Rules;

namespace Application.Fetaures.BasketItems.Queries.GetById;

public class GetByIdBasketItemQuery : IRequest<GetByIdBasketItemResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBasketItemQueryHandler(IMapper mapper, IBasketItemRepository basketItemRepository, BasketItemBusinessRules basketItemBusinessRules) : IRequestHandler<GetByIdBasketItemQuery, GetByIdBasketItemResponse>
    {
        public async Task<GetByIdBasketItemResponse> Handle(GetByIdBasketItemQuery request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await basketItemRepository.GetAsync(predicate: bi => bi.Id == request.Id, cancellationToken: cancellationToken);
            basketItemBusinessRules.BasketItemShouldExist(basketItem);

            GetByIdBasketItemResponse response = mapper.Map<GetByIdBasketItemResponse>(basketItem);
            return response;
        }
    }
}
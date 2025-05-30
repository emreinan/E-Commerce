using Application.Fetaures.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Baskets.Queries.GetById;

public class GetByIdBasketQuery : IRequest<GetByIdBasketResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBasketQueryHandler(IMapper mapper,IBasketRepository basketRepository, BasketBusinessRules basketBusinessRules) : IRequestHandler<GetByIdBasketQuery, GetByIdBasketResponse>
    {
        public async Task<GetByIdBasketResponse> Handle(GetByIdBasketQuery request, CancellationToken cancellationToken)
        {
            Basket? basket = await basketRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            basketBusinessRules.BasketShouldExistWhenSelected(basket);

            var response = mapper.Map<GetByIdBasketResponse>(basket);
            return response;

        }
    }
}
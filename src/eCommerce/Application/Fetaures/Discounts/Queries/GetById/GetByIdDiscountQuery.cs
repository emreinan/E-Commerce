using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Discounts.Queries.GetById;

public class GetByIdDiscountQuery : IRequest<GetByIdDiscountResponse>
{
    public Guid Id { get; set; }

    public class GetByIdDiscountQueryHandler(IMapper mapper,
                                             IDiscountRepository discountRepository,
                                             DiscountBusinessRules discountBusinessRules) : IRequestHandler<GetByIdDiscountQuery, GetByIdDiscountResponse>
    {
        public async Task<GetByIdDiscountResponse> Handle(GetByIdDiscountQuery request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            discountBusinessRules.DiscountShouldExistWhenSelected(discount);

            var response = mapper.Map<GetByIdDiscountResponse>(discount);
            return response;
        }
    }
}
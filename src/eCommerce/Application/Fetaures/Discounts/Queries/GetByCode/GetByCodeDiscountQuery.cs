using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Discounts.Queries.GetByCode;

public class GetByCodeDiscountQuery : IRequest<GetByCodeDiscountResponse>
{
    public required string Code { get; set; }

    public class GetDiscountByCodeQueryHandler(
        IDiscountRepository discountRepository,
        IMapper mapper,
        DiscountBusinessRules discountBusinessRules) : IRequestHandler<GetByCodeDiscountQuery, GetByCodeDiscountResponse>
    {
        public async Task<GetByCodeDiscountResponse> Handle(GetByCodeDiscountQuery request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(
                predicate: d => d.Code.Equals(request.Code.ToUpperInvariant()),
                enableTracking: false,
                cancellationToken: cancellationToken);

            discountBusinessRules.DiscountShouldExistWhenSelected(discount); 

            return mapper.Map<GetByCodeDiscountResponse>(discount);
        }
    }
}

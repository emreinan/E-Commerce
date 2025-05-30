using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Discounts.Queries.GetList;

public class GetListDiscountQuery : IRequest<IEnumerable<GetListDiscountListItemDto>>
{
    public class GetListDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper) : IRequestHandler<GetListDiscountQuery, IEnumerable<GetListDiscountListItemDto>>
    {
    public async Task<IEnumerable<GetListDiscountListItemDto>> Handle(GetListDiscountQuery request, CancellationToken cancellationToken)
        {
        IEnumerable<Discount> discounts = await discountRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<IEnumerable<GetListDiscountListItemDto>>(discounts);
            return response;
        }
    }
}
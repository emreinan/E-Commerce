using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Products.Queries.GetList;

public class GetListProductQuery : IRequest<IEnumerable<GetListProductListItemDto>>
{
    public class GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetListProductQuery, IEnumerable<GetListProductListItemDto>>
    {
        public async Task<IEnumerable<GetListProductListItemDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await productRepository.GetListAsync(
                cancellationToken: cancellationToken
            );

            var response = mapper.Map<IEnumerable<GetListProductListItemDto>>(products);
            return response;
        }
    }
}
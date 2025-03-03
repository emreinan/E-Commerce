using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Products.Rules;

namespace Application.Fetaures.Products.Queries.GetById;

public class GetByIdProductQuery : IRequest<GetByIdProductResponse>
{
    public Guid Id { get; set; }

    public class GetByIdProductQueryHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules) : IRequestHandler<GetByIdProductQuery, GetByIdProductResponse>
    {
        public async Task<GetByIdProductResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            productBusinessRules.ProductShouldExistWhenSelected(product);

            GetByIdProductResponse response = mapper.Map<GetByIdProductResponse>(product);
            return response;
        }
    }
}
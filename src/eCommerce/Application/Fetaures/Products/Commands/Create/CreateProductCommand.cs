using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Products.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Products.Commands.Create;

public sealed record CreateProductCommand(
    Guid StoreId,
    Guid CategoryId,
    string Name,
    decimal Price,
    string? Details,
    int StockAmount,
    bool Enabled
) : IRequest<CreatedProductResponse>, ITransactionalRequest
{
    public sealed class Handler(
        IMapper mapper,
        IProductRepository productRepository,
        ProductBusinessRules productBusinessRules
    ) : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {
        public async Task<CreatedProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await productBusinessRules.StoreShouldExist(request.StoreId, cancellationToken);
            await productBusinessRules.CategoryShouldExist(request.CategoryId, cancellationToken);
            await productBusinessRules.ProductNameBeUnique(request.Name, cancellationToken);

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product, cancellationToken);

            var response = mapper.Map<CreatedProductResponse>(product);
            return response;
        }
    }
}

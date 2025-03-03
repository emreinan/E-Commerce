using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Products.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Products.Commands.Create;

public class CreateProductCommand : IRequest<CreatedProductResponse>, ITransactionalRequest
{
    public required Guid SellerId { get; set; }
    public required Guid CategoryId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public string? Details { get; set; }
    public required int StockAmount { get; set; }
    public required bool Enabled { get; set; }

    public class CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository,
                                     ProductBusinessRules productBusinessRules) : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {
        public async Task<CreatedProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await productBusinessRules.ProductNameBeUnique(request.Name, cancellationToken);

            Product product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);

            CreatedProductResponse response = mapper.Map<CreatedProductResponse>(product);
            return response;
        }
    }
}
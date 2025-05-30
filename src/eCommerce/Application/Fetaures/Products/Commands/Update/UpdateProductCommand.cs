using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Products.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Products.Commands.Update;

public class UpdateProductCommand : IRequest<UpdatedProductResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateProductRequest Request { get; set; } = default!;

    public class UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository,
                                     ProductBusinessRules productBusinessRules) : IRequestHandler<UpdateProductCommand, UpdatedProductResponse>
    {
        public async Task<UpdatedProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            productBusinessRules.ProductShouldExistWhenSelected(product);

            await productBusinessRules.StoreShouldExist(request.Request.StoreId, cancellationToken);
            await productBusinessRules.CategoryShouldExist(request.Request.CategoryId, cancellationToken);
            await productBusinessRules.ProductNameBeUniqueOnUpdate(request.Request.Name, request.Id, cancellationToken);

            product = mapper.Map(request.Request, product);

            await productRepository.UpdateAsync(product!, cancellationToken);

            UpdatedProductResponse response = mapper.Map<UpdatedProductResponse>(product);
            return response;
        }
    }
}
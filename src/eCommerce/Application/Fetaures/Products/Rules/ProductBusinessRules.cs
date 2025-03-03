using Application.Fetaures.Products.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Products.Rules;

public class ProductBusinessRules(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void ProductShouldExistWhenSelected(Product? product)
    {
        if (product == null)
            throw new BusinessException(ProductsBusinessMessages.ProductNotExists);
    }

    public async Task ProductIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        ProductShouldExistWhenSelected(product);
    }
    public async Task ProductNameBeUnique(string name, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetAsync(
            predicate: p => p.Name == name,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        if (product is not null)
            throw new BusinessException(ProductsBusinessMessages.ProductNameAlreadyExists);
    }
}
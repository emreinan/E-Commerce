using Application.Fetaures.Products.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Fetaures.Products.Rules;

public class ProductBusinessRules(IProductRepository productRepository,
                                  ICategoryRepository categoryRepository,
                                  IStoreRepository storeRepository,
                                  IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
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
    public async Task StoreShouldExist(Guid storeId, CancellationToken cancellationToken)
    {
        var exists = await storeRepository.AnyAsync(predicate: p => p.Id == storeId, cancellationToken: cancellationToken);
        if (!exists)
            throw new BusinessException(ProductsBusinessMessages.StoreNotExists);
    }

    public async Task CategoryShouldExist(Guid categoryId, CancellationToken cancellationToken)
    {
        var exists = await categoryRepository.AnyAsync(
            predicate: p => p.Id == categoryId,
            cancellationToken: cancellationToken
        );
        if (!exists)
            throw new BusinessException(ProductsBusinessMessages.CategoryNotExists);
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
    public async Task ProductNameBeUniqueOnUpdate(string name, Guid currentProductId, CancellationToken cancellationToken)
    {
        Product? existing = await productRepository.GetAsync(
            predicate: p => p.Name == name && p.Id != currentProductId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        if (existing is not null)
            throw new BusinessException(ProductsBusinessMessages.ProductNameAlreadyExists);
    }
    
}
using Application.Services.Repositories;
using Domain.Entities;
using Application.Fetaures.ProductImages.Constants;
using Core.Application.Rules;
using Microsoft.AspNetCore.Http;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Fetaures.ProductImages.Rules;

public class ProductImageBusinessRules(IProductImageRepository productImageRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void ProductImageShouldExistWhenSelected(ProductImage? productImage)
    {
        if (productImage is null)
            throw new BusinessException(ProductImagesBusinessMessages.ProductImageNotExists);
    }

    public async Task ProductImageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductImage? productImage = await productImageRepository.GetAsync(
            predicate: pi => pi.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        ProductImageShouldExistWhenSelected(productImage);
    }
}
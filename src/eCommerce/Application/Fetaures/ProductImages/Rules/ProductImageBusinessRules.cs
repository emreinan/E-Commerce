using Application.Fetaures.ProductImages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

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
    public async Task EnsureSingleMainImageAllowed(Guid productId, bool isAnyNewMain, CancellationToken cancellationToken)
    {
        if (!isAnyNewMain) return;

        bool mainExists = await productImageRepository.AnyAsync(
            predicate: p => p.ProductId == productId && p.IsMain,
            cancellationToken: cancellationToken
        );

        if (mainExists)
            throw new BusinessException(ProductImagesBusinessMessages.ProductImageAlreadyMainImage);
    }
    public async Task UnsetOtherMainImages(Guid productId, Guid currentImageId, CancellationToken cancellationToken)
    {
        var mainImages = await productImageRepository.GetListAsync(
            predicate: p => p.ProductId == productId && p.IsMain && p.Id != currentImageId,
            cancellationToken: cancellationToken
        );

        foreach (var image in mainImages)
        {
            image.IsMain = false;
            await productImageRepository.UpdateAsync(image, cancellationToken: cancellationToken);
        }
    }
    public async Task EnsureProductImageCanBeDeleted(ProductImage image, CancellationToken cancellationToken)
    {
        var imagesOfProduct = await productImageRepository.GetListAsync(
            predicate: p => p.ProductId == image.ProductId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (imagesOfProduct.Count == 1)
            throw new BusinessException(ProductImagesBusinessMessages.ProductImageCannotBeDeletedIfOnlyOneExists);

        if (image.IsMain)
            throw new BusinessException(ProductImagesBusinessMessages.ProductImageMainCannotBeDeleted);
    }
}
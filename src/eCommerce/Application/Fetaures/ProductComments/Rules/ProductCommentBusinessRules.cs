using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Application.Fetaures.ProductComments.Constants;

namespace Application.Fetaures.ProductComments.Rules;

public class ProductCommentBusinessRules(IProductCommentRepository productCommentRepository,
                                         IOrderRepository orderRepository,
                                         IProductRepository productRepository,
                                         IUserRepository userRepository,
                                         IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void ProductCommentShouldExistWhenSelected(ProductComment? productComment)
    {
        if (productComment is null)
            throw new BusinessException(ProductCommentsBusinessMessages.ProductCommentNotExists);
    }

    public async Task ProductCommentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductComment? productComment = await productCommentRepository.GetAsync(
            predicate: pc => pc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        ProductCommentShouldExistWhenSelected(productComment);
    }
    public async Task EnsureUserPurchasedProduct(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        bool hasPurchased = await orderRepository.AnyAsync(
            predicate: o =>
                o.UserId == userId &&
                o.OrderItems.Any(oi => oi.ProductId == productId) &&
                o.Status == Domain.Enums.OrderStatus.Completed, // Tamamlanmýþ sipariþ
            cancellationToken: cancellationToken
        );

        if (!hasPurchased)
            throw new BusinessException(ProductCommentsBusinessMessages.UserHasNotPurchasedProduct);
    }

    public async Task EnsureProductExists(Guid productId, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(p => p.Id == productId, cancellationToken: cancellationToken);
        if (product is null)
            throw new BusinessException(ProductCommentsBusinessMessages.ProductNotExists);
    }

    public async Task EnsureUserExists(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(u => u.Id == userId, cancellationToken: cancellationToken);
        if (user is null)
            throw new BusinessException(ProductCommentsBusinessMessages.UserNotExists);
    }

}
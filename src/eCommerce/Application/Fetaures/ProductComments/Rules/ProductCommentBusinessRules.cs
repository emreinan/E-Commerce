using Application.Features.ProductComments.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.ProductComments.Rules;

public class ProductCommentBusinessRules(IProductCommentRepository productCommentRepository,IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
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
}
using Application.Fetaures.Discounts.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Discounts.Rules;

public class DiscountBusinessRules(IDiscountRepository discountRepository, 
                                   IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void DiscountShouldExistWhenSelected(Discount? discount)
    {
        if (discount is null)
            throw new BusinessException(DiscountsBusinessMessages.DiscountNotExists);
    }

    public async Task DiscountIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Discount? discount = await discountRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken);
        DiscountShouldExistWhenSelected(discount);
    }
    public async Task DýscountCodeShouldBeUnique(string code)
    {
        Discount? discount = await discountRepository.GetAsync(
            predicate: d => d.Code == code);
        if (discount is not null)
            throw new BusinessException(DiscountsBusinessMessages.DiscountCodeShouldBeUnique);
    }
}
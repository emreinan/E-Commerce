using Application.Fetaures.Discounts.Commands.Update;
using Application.Fetaures.Discounts.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;
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

    public void DiscountShouldBeUsable(Discount discount, decimal orderTotal)
    {
        if (!discount.IsActive)
            throw new BusinessException(DiscountsBusinessMessages.DiscountNotUsable);

        if (discount.EndDate <= DateTime.UtcNow)
            throw new BusinessException(DiscountsBusinessMessages.DiscountCodeExpired);

        if (discount.UsageLimit <= 0)
            throw new BusinessException(DiscountsBusinessMessages.DiscountCodeHasReachedUsageLimir);

        if (orderTotal < discount.MinOrderAmount)
            throw new BusinessException($"Order total must be at least {discount.MinOrderAmount} to apply this discount.");
    }


    public void DiscountShouldNotBeAlreadyActive(Discount discount)
    {
        if (discount.IsActive)
            throw new BusinessException(DiscountsBusinessMessages.DiscountAlreadyActive);
    }

    public void DiscountShouldNotBeAlreadyInactive(Discount discount)
    {
        if (!discount.IsActive)
            throw new BusinessException(DiscountsBusinessMessages.DiscountAlreadyInactive);
    }

}
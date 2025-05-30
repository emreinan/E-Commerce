using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Fetaures.Discounts.Commands.Apply;

public class ApplyDiscountCommand : IRequest<AppliedDiscountResponse>
{
    public required string Code { get; set; }
    public required decimal OrderTotal { get; set; }

    public class ApplyDiscountCommandHandler(
        IMapper mapper,
        IDiscountRepository discountRepository,
        DiscountBusinessRules discountBusinessRules)
        : IRequestHandler<ApplyDiscountCommand, AppliedDiscountResponse>
    {
        public async Task<AppliedDiscountResponse> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(
                d => d.Code == request.Code.ToUpperInvariant() && d.IsActive,
                cancellationToken: cancellationToken);

            discountBusinessRules.DiscountShouldExistWhenSelected(discount);
            discountBusinessRules.DiscountShouldBeUsable(discount!, request.OrderTotal);

            decimal calculatedDiscount = discount!.Type switch
            {
                DiscountType.Amount => discount.Value,
                DiscountType.Percentage => request.OrderTotal * discount.Value / 100,
                _ => 0
            };

            calculatedDiscount = Math.Min(calculatedDiscount, request.OrderTotal);
            decimal newTotal = request.OrderTotal - calculatedDiscount;

            var response = mapper.Map<AppliedDiscountResponse>(discount);
            response.TotalDiscountValue = calculatedDiscount;
            response.NewTotal = newTotal;

            return response;
        }
    }
}

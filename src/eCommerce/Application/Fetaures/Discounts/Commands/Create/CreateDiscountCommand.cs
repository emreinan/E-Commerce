using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Fetaures.Discounts.Commands.Create;

public class CreateDiscountCommand : IRequest<CreatedDiscountResponse>, ITransactionalRequest
{
    public required string Code { get; set; }
    public required decimal Value { get; set; }
    public required DiscountType Type { get; set; }
    public required decimal MinOrderAmount { get; set; }
    public required int UsageLimit { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public class CreateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository,
                                     DiscountBusinessRules discountBusinessRules) : IRequestHandler<CreateDiscountCommand, CreatedDiscountResponse>
    {
        public async Task<CreatedDiscountResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            request.Code = request.Code.ToUpperInvariant();

            await discountBusinessRules.DýscountCodeShouldBeUnique(request.Code);

            Discount discount = mapper.Map<Discount>(request);

            await discountRepository.AddAsync(discount, cancellationToken);

            CreatedDiscountResponse response = mapper.Map<CreatedDiscountResponse>(discount);
            return response;
        }
    }
}
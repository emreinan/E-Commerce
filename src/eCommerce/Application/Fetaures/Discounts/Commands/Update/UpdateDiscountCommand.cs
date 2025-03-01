using Application.Fetaures.Discounts.Constants;
using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Discounts.Commands.Update;

public class UpdateDiscountCommand : IRequest<UpdatedDiscountResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateDiscountRequest Request { get; set; } = default!;

    public class UpdateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository,
                                     DiscountBusinessRules discountBusinessRules) : IRequestHandler<UpdateDiscountCommand, UpdatedDiscountResponse>
    {
        public async Task<UpdatedDiscountResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            discountBusinessRules.DiscountShouldExistWhenSelected(discount);
            discount = mapper.Map(request, discount);

            await discountRepository.UpdateAsync(discount!);

            UpdatedDiscountResponse response = mapper.Map<UpdatedDiscountResponse>(discount);
            return response;
        }
    }
}
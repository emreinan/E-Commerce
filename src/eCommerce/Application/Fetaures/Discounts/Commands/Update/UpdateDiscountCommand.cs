using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Discounts.Commands.Update;

public class UpdateDiscountCommand : IRequest<UpdatedDiscountResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateDiscountRequest Request { get; set; } = default!;

    public class UpdateDiscountCommandHandler(
        IMapper mapper,
        IDiscountRepository discountRepository,
        DiscountBusinessRules discountBusinessRules)
        : IRequestHandler<UpdateDiscountCommand, UpdatedDiscountResponse>
    {
        public async Task<UpdatedDiscountResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(
                predicate: d => d.Id == request.Id,
                cancellationToken: cancellationToken);

            discountBusinessRules.DiscountShouldExistWhenSelected(discount);

            mapper.Map(request.Request, discount);

            await discountRepository.UpdateAsync(discount!);

            return mapper.Map<UpdatedDiscountResponse>(discount);
        }
    }
}

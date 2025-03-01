using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Discounts.Commands.Delete;

public class DeleteDiscountCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteDiscountCommandHandler(IDiscountRepository discountRepository,
                                     DiscountBusinessRules discountBusinessRules) : IRequestHandler<DeleteDiscountCommand>
    {
        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            discountBusinessRules.DiscountShouldExistWhenSelected(discount);

            await discountRepository.DeleteAsync(discount!, cancellationToken: cancellationToken);
        }
    }
}
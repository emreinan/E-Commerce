using Application.Fetaures.Discounts.Commands.Delete;
using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Discounts.Commands.Deactivate;

public class DeactivateDiscountCommand : IRequest
{
    public required Guid Id { get; set; }

    public class DeactivateDiscountCommandHandler(
        IDiscountRepository discountRepository,
        DiscountBusinessRules discountBusinessRules) : IRequestHandler<DeactivateDiscountCommand>
    {
        public async Task Handle(DeactivateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(
                d => d.Id == request.Id, cancellationToken: cancellationToken);

            discountBusinessRules.DiscountShouldExistWhenSelected(discount!);
            discountBusinessRules.DiscountShouldNotBeAlreadyInactive(discount!);

            discount!.IsActive = false;
            await discountRepository.UpdateAsync(discount, cancellationToken);
        }
    }
}

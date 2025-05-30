using Application.Fetaures.Discounts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Discounts.Commands.Activate;

public class ActivateDiscountCommand : IRequest{
    public required Guid Id { get; set; }

    public class ActivateDiscountCommandHandler(
        IDiscountRepository discountRepository,
        DiscountBusinessRules discountBusinessRules) : IRequestHandler<ActivateDiscountCommand>
    {
        public async Task Handle(ActivateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(
                d => d.Id == request.Id, cancellationToken: cancellationToken);

            discountBusinessRules.DiscountShouldExistWhenSelected(discount!);

            discountBusinessRules.DiscountShouldNotBeAlreadyActive(discount!);

            discount!.IsActive = true;
            await discountRepository.UpdateAsync(discount, cancellationToken);
        }
    }
}

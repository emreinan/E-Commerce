using Application.Fetaures.BasketItems.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.BasketItems.Commands.Delete;

public class DeleteBasketItemCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteBasketItemCommandHandler(IBasketItemRepository basketItemRepository,
                                     BasketItemBusinessRules basketItemBusinessRules) : IRequestHandler<DeleteBasketItemCommand>
    {
        public async Task Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await basketItemRepository.GetAsync(predicate: bi => bi.Id == request.Id, cancellationToken: cancellationToken);
            basketItemBusinessRules.BasketItemShouldExist(basketItem);

            await basketItemRepository.DeleteAsync(basketItem!, cancellationToken: cancellationToken);
        }
    }
}
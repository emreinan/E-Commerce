using Application.Fetaures.Baskets.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Fetaures.Baskets.Commands.Delete;

public record DeleteBasketCommand(Guid Id) : IRequest,  ITransactionalRequest
{
    public class DeleteBasketCommandHandler(IBasketRepository basketRepository,
                                     BasketBusinessRules basketBusinessRules) : IRequestHandler<DeleteBasketCommand>
    {
        public async Task Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            Basket? basket = await basketRepository.GetAsync(
                predicate: b => b.Id == request.Id,
                include: b => b.Include(b => b.BasketItems),
                cancellationToken: cancellationToken);
            basketBusinessRules.BasketShouldExistWhenSelected(basket);

            basket!.IsActive = false; 

            if (basket!.BasketItems != null)
            {
                foreach (var item in basket.BasketItems)
                {
                    item.DeletedDate = DateTime.UtcNow;
                }
            }

            await basketRepository.DeleteAsync(basket!, cancellationToken: cancellationToken);
        }
    }
}
using Application.Fetaures.Baskets.Constants;
using Application.Fetaures.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Baskets.Commands.Delete;

public class DeleteBasketCommand : IRequest,  ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteBasketCommandHandler(IBasketRepository basketRepository,
                                     BasketBusinessRules basketBusinessRules) : IRequestHandler<DeleteBasketCommand>
    {
        public async Task Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            Basket? basket = await basketRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            basketBusinessRules.BasketShouldExistWhenSelected(basket);

            await basketRepository.DeleteAsync(basket!);
        }
    }
}
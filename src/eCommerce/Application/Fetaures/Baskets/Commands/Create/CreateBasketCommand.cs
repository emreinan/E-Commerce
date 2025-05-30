using Application.Fetaures.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Baskets.Commands.Create;

public record CreateBasketCommand(Guid? UserId) : IRequest<CreatedBasketResponse>, ITransactionalRequest
{
    public class CreateBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository, BasketBusinessRules basketBusinessRules) : IRequestHandler<CreateBasketCommand, CreatedBasketResponse>
    {
        public async Task<CreatedBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var guestId = basketBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.UserId);

            var basket = await basketBusinessRules.GetOrCreateBasketAsync(request.UserId, guestId);
            if (basket is null)
            {
                basket = new Basket
                {
                    UserId = request.UserId,
                    GuestId = string.IsNullOrEmpty(guestId) ? null : guestId,
                    IsActive = true,
                };
                await basketRepository.AddAsync(basket, cancellationToken);
            }

            CreatedBasketResponse response = mapper.Map<CreatedBasketResponse>(basket);
            return response;
        }
    }
}

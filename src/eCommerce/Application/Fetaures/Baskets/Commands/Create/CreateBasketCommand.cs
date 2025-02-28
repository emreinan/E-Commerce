using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Features.Baskets.Commands.Create;

public class CreateBasketCommand : IRequest<CreatedBasketResponse>, ITransactionalRequest
{
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }

    public class CreateBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository) : IRequestHandler<CreateBasketCommand, CreatedBasketResponse>
    {
        public async Task<CreatedBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            Basket basket = mapper.Map<Basket>(request);

            await basketRepository.AddAsync(basket);

            CreatedBasketResponse response = mapper.Map<CreatedBasketResponse>(basket);
            return response;
        }
    }
}
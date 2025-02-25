using Application.Fetaures.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.BasketItems.Commands.Create;

public class CreateBasketItemCommand : IRequest<CreatedBasketItemResponse>, ITransactionalRequest
{
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
    public required decimal Price { get; set; }

    public class CreateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                     BasketItemBusinessRules basketItemBusinessRules) : IRequestHandler<CreateBasketItemCommand, CreatedBasketItemResponse>
    {
        public async Task<CreatedBasketItemResponse> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            // 1 Kullanıcı Yoksa Misafir Id Oluştur
            var guestId = basketItemBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.UserId);
            request.GuestId = guestId;

            // 2 Ürün ve Stok Kontrolü
            await basketItemBusinessRules.CheckProductAndStockAsync(request.ProductId, request.Quantity);

            // 3 Kullanıcı veya Misafir Sepeti varsa al yoksa Oluştur
            var basket = await basketItemBusinessRules.GetOrCreateBasketAsync(request.UserId, request.GuestId, cancellationToken);

            // 4 Sepette Aynı Ürün Var mı Kontrol Et
            BasketItem? existingItem = await basketItemRepository.GetAsync(bi => bi.BasketId == basket.Id && bi.ProductId == request.ProductId, cancellationToken: cancellationToken);
            if (existingItem is not null)
            {
                existingItem.Quantity += request.Quantity;
                await basketItemRepository.UpdateAsync(existingItem, cancellationToken);
                return mapper.Map<CreatedBasketItemResponse>(existingItem);
            }

            // 5 Yeni Sepet Öğesi Ekle
            BasketItem basketItem = new()
            {
                BasketId = basket.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Price = request.Price
            };
            await basketItemRepository.AddAsync(basketItem, cancellationToken);

            return mapper.Map<CreatedBasketItemResponse>(basketItem);
        }
    }
}
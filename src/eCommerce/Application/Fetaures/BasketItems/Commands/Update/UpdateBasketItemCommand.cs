using Application.Fetaures.BasketItems.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.BasketItems.Commands.Update;

public class UpdateBasketItemCommand : IRequest<UpdatedBasketItemResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateBasketItemRequest Request { get; set; } = default!;

    public class UpdateBasketItemCommandHandler( IBasketItemRepository basketItemRepository,
                                     BasketItemBusinessRules basketItemBusinessRules) : IRequestHandler<UpdateBasketItemCommand, UpdatedBasketItemResponse>
    {
        public async Task<UpdatedBasketItemResponse> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            // Sepet Var mý Kontrol Et
            BasketItem? basketItem = await basketItemRepository.GetAsync(bi => bi.Id == request.Id, cancellationToken: cancellationToken);

            // Sepette böyle bir Sepet Öðesi Var mý Kontrol Et
            basketItemBusinessRules.BasketItemShouldExist(basketItem);

            // Sadece quantity güncellenecek, quantity -1 veya +1 gelecek, bu deðer eklenecek.
            basketItem!.Quantity += request.Request.Quantity;

            // Ürün ve Stok Kontrolü
            await basketItemBusinessRules.CheckProductAndStockAsync(basketItem.ProductId, basketItem.Quantity);

            // Eðer basketitem 0 olduysa silinecek.
            await basketItemBusinessRules.ReduceOrDeleteBasketItemAsync(basketItem, cancellationToken);

            // Eðer Basket içinde ürün kalmadýysa sepet silinecek.
            await basketItemBusinessRules.CheckAndDeleteBasketIfEmptyAsync(basketItem.BasketId, cancellationToken);

            return new UpdatedBasketItemResponse
            {
                Id = basketItem.Id,
                BasketId = basketItem.BasketId,
                ProductId = basketItem.ProductId,
                Quantity = basketItem.Quantity,
                Price = basketItem.Price
            };
        }
    }
}
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
            // Sepet Var m� Kontrol Et
            BasketItem? basketItem = await basketItemRepository.GetAsync(bi => bi.Id == request.Id, cancellationToken: cancellationToken);

            // Sepette b�yle bir Sepet ��esi Var m� Kontrol Et
            basketItemBusinessRules.BasketItemShouldExist(basketItem);

            // Sadece quantity g�ncellenecek, quantity -1 veya +1 gelecek, bu de�er eklenecek.
            basketItem!.Quantity += request.Request.Quantity;

            // �r�n ve Stok Kontrol�
            await basketItemBusinessRules.CheckProductAndStockAsync(basketItem.ProductId, basketItem.Quantity);

            // E�er basketitem 0 olduysa silinecek.
            await basketItemBusinessRules.ReduceOrDeleteBasketItemAsync(basketItem, cancellationToken);

            // E�er Basket i�inde �r�n kalmad�ysa sepet silinecek.
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
using Application.Fetaures.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.BasketItems.Commands.Update;

public class UpdateBasketItemCommand : IRequest<UpdatedBasketItemResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required UpdateBasketItemRequest Request { get; set; }

    public class UpdateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                     BasketItemBusinessRules basketItemBusinessRules) : IRequestHandler<UpdateBasketItemCommand, UpdatedBasketItemResponse>
    {
        public async Task<UpdatedBasketItemResponse> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            // Sepet Var mý Kontrol Et
            BasketItem? basketItem = await basketItemRepository.GetAsync(bi => bi.Id == request.Id, cancellationToken: cancellationToken);

            // Sepette böyle bir Sepet Öðesi Var mý Kontrol Et
            basketItemBusinessRules.BasketItemShouldExist(basketItem);

            // Sadece quantity güncellenecek, örnek quantity -1 veya +1 gelecek, bu deðer eklenecek.
            basketItem!.Quantity += request.Request.Quantity;

            // Ürün ve Stok Kontrolü
            await basketItemBusinessRules.CheckProductAndStockAsync(basketItem.ProductId, basketItem.Quantity);

            // Eðer basketitem 0 olduysa silinecek.
            await basketItemBusinessRules.ReduceOrDeleteBasketItemAsync(basketItem, cancellationToken);

            await basketItemRepository.UpdateAsync(basketItem, cancellationToken);

            var response = mapper.Map<UpdatedBasketItemResponse>(basketItem);
            return response;
        }
    }
}
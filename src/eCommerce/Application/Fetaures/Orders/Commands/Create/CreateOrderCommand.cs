using Application.Fetaures.Addresses.Rules;
using Application.Fetaures.Baskets.Rules;
using Application.Fetaures.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Fetaures.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<CreatedOrderResponse>, ITransactionalRequest
{
    public Guid? UserId { get; set; }
    public required Guid ShippingAddressId { get; set; }
    public Guid? DiscountId { get; set; }
    public required decimal ShippingCost { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }

    public string? GuestEmail { get; set; }
    public string? GuestPhoneNumber { get; set; }

    public class CreateOrderCommandHandler(
    IMapper mapper,
    IOrderRepository orderRepository,
    OrderBusinessRules orderBusinessRules,
    AddressBusinessRules addressBusinessRules,
    BasketBusinessRules basketBusinessRules,
    IBasketRepository basketRepository,
    IBasketItemRepository basketItemRepository
) : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Gerekli address ve discount kontrolleri
            await addressBusinessRules.AddressIsExists(request.ShippingAddressId, cancellationToken);
            await orderBusinessRules.DiscountShouldBeUsable(request.DiscountId, cancellationToken);

            // 2. GuestId'yi, kullanıcı giriş yapmadıysa cookie'den veya oluşturulmuş olarak al
            var guestId = orderBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.UserId);

            // 3. Basket’i çek: eğer UserId mevcutsa o, yoksa guestId ile
            Basket? basket = await basketRepository.GetAsync(
                predicate: b =>
                    (request.UserId.HasValue && b.UserId == request.UserId) ||
                    (!request.UserId.HasValue && !string.IsNullOrEmpty(guestId) && b.GuestId == guestId),
                include: q => q.Include(b => b.BasketItems)
                               .ThenInclude(bi => bi.Product)
                               .Include(b => b.Discount),
                enableTracking: true,
                cancellationToken: cancellationToken
            );

            basketBusinessRules.BasketShouldExistWhenSelected(basket);
            basketBusinessRules.BasketShouldHaveItems(basket!);

            // 4. BasketItem'lerden OrderItem üretimi
            var orderItems = basket!.BasketItems.Select(bi => new OrderItem
            {
                ProductId = bi.ProductId,
                // OrderItem'a sipariş anındaki ürün bilgileri yazılır
                ProductNameAtOrderTime = bi.Product.Name,
                ProductPriceAtOrderTime = bi.UnitPrice,
                Quantity = bi.Quantity,
            }).ToList();

            // 5. Hesaplamalar: Sepet toplamı, KDV, indirim ve final tutar
            decimal basketSubtotal = basket.BasketItems.Sum(bi => bi.UnitPrice * bi.Quantity);
            // TODO: Configurationdan al
            decimal taxAmount = basketSubtotal * 0.18m; // Örneğin %18 KDV
            decimal discountAmount = 0m;

            if (basket.Discount is not null && basket.Discount.IsUsable)
            {
                var totalBasketPrice = basket.BasketItems.Sum(item => item.UnitPrice * item.Quantity);

                if (totalBasketPrice >= basket.Discount.MinOrderAmount)
                {
                    discountAmount = basket.Discount.Type switch
                    {
                        DiscountType.Amount => basket.Discount.Value,
                        DiscountType.Percentage => totalBasketPrice * (basket.Discount.Value / 100m),
                        _ => 0m
                    };
                }
            }

            // Normalde FinalAmount'ı client göndermemeli, backend hesaplamalı
            decimal computedFinalAmount = basketSubtotal + request.ShippingCost + taxAmount - discountAmount;

            // 6. Order entity'sine mapleme ve ek bilgilerin doldurulması
            Order order = mapper.Map<Order>(request);
            order.OrderCode = GenerateOrderCode();
            order.OrderDate = DateTime.UtcNow;
            order.TaxAmount = taxAmount;
            order.FinalAmount = computedFinalAmount;

            // Eğer kullanıcı kayıtlı değilse, guest bilgileri komuttan alınıyor
            if (!request.UserId.HasValue)
            {
                order.GuestId = guestId;
                order.GuestEmail = request.GuestEmail;
                order.GuestPhoneNumber = request.GuestPhoneNumber;
            }
            // Aksi durumda; order.UserId, mapper üzerinden ayarlanacaktır.

            // OrderItem'ları siparişe ekle
            order.OrderItems = orderItems;

            // 7. Order kaydı veritabanına eklenir
            await orderRepository.AddAsync(order, cancellationToken);

            // 8. Sipariş verildikten sonra sepet temizlenir
            foreach (var item in basket.BasketItems.ToList())
            {
                await basketItemRepository.DeleteAsync(item,cancellationToken: cancellationToken); // Soft veya hard delete'e göre değişebilir
            }
            //foreach (var item in basket.BasketItems)
            //{
            //    item.DeletedDate = DateTime.UtcNow;
            //    await basketItemRepository.UpdateAsync(item, cancellationToken);
            //}

            await basketRepository.UpdateAsync(basket, cancellationToken);

            // 9. Response oluşturulur
            CreatedOrderResponse response = mapper.Map<CreatedOrderResponse>(order);
            return response;
        }
        private string GenerateOrderCode()
        {
            return $"{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
    }
}
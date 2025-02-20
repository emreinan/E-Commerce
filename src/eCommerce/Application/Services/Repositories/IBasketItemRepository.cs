using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBasketItemRepository : IAsyncRepository<BasketItem, Guid>, IRepository<BasketItem, Guid>
{
}
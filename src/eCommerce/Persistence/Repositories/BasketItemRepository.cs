using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BasketItemRepository(AppDbContext context) : EfRepositoryBase<BasketItem, Guid, AppDbContext>(context), IBasketItemRepository
{
}
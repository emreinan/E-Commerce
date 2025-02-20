using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BasketRepository(AppDbContext context) : EfRepositoryBase<Basket, Guid, AppDbContext>(context), IBasketRepository
{
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderRepository(AppDbContext context) : EfRepositoryBase<Order, Guid, AppDbContext>(context), IOrderRepository
{
}
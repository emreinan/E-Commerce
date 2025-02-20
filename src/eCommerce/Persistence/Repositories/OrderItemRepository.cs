using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderItemRepository(AppDbContext context) : EfRepositoryBase<OrderItem, Guid, AppDbContext>(context), IOrderItemRepository
{
}
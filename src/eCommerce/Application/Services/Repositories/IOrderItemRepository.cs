using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IOrderItemRepository : IAsyncRepository<OrderItem, Guid>, IRepository<OrderItem, Guid>
{
}
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IOrderRepository : IAsyncRepository<Order, Guid>, IRepository<Order, Guid>
{
}
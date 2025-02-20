using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDiscountRepository : IAsyncRepository<Discount, Guid>, IRepository<Discount, Guid>
{
}
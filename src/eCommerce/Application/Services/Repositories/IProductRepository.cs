using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductRepository : IAsyncRepository<Product, Guid>, IRepository<Product, Guid>
{
}
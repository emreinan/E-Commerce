using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBasketRepository : IAsyncRepository<Basket, Guid>, IRepository<Basket, Guid>
{
}
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStoreRepository : IAsyncRepository<Store, Guid>, IRepository<Store, Guid>
{
}
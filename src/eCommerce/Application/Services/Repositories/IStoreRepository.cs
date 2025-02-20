using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IStoreRepository : IAsyncRepository<User, Guid>, IRepository<User, Guid> { }
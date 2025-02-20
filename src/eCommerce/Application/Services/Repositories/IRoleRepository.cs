using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IRoleRepository : IAsyncRepository<Role, Guid>, IRepository<Role, Guid> { }

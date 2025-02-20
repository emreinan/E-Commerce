using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IUserRoleRepository : IAsyncRepository<UserRole, Guid>, IRepository<UserRole, Guid>
{
    Task<IList<Role>> GetRolesByUserIdAsync(Guid userId);
}

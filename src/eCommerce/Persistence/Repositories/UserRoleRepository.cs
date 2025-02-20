using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRoleRepository(AppDbContext context)
        : EfRepositoryBase<UserRole, Guid, AppDbContext>(context),
        IUserRoleRepository
{
    public async Task<IList<Role>> GetRolesByUserIdAsync(Guid userId)
    {
        List<Role> operationClaims = await Query()
            .AsNoTracking()
            .Where(p => p.UserId.Equals(userId))
            .Select(p => new Role { Id = p.RoleId, Name = p.Role.Name })
            .ToListAsync();
        return operationClaims;
    }
}

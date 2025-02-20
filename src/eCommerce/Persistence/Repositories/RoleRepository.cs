using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RoleRepository(AppDbContext context) : EfRepositoryBase<Role, Guid, AppDbContext>(context), IRoleRepository
{
}

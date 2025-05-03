using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StoreRepository(AppDbContext context) : EfRepositoryBase<Store, Guid, AppDbContext>(context), IStoreRepository
{
}
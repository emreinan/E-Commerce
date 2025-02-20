using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AddressRepository(AppDbContext context) : EfRepositoryBase<Address, Guid, AppDbContext>(context), IAddressRepository
{
}
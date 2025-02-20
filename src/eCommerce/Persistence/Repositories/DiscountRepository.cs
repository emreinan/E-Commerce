using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DiscountRepository(AppDbContext context) : EfRepositoryBase<Discount, Guid, AppDbContext>(context), IDiscountRepository
{
}
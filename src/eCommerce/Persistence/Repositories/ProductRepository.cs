using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductRepository(AppDbContext context) : EfRepositoryBase<Product, Guid, AppDbContext>(context), IProductRepository
{
}
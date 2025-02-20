using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductImageRepository(AppDbContext context) : EfRepositoryBase<ProductImage, Guid, AppDbContext>(context), IProductImageRepository
{
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductCommentRepository(AppDbContext context) : EfRepositoryBase<ProductComment, Guid, AppDbContext>(context), IProductCommentRepository
{
}
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryRepository(AppDbContext context) : EfRepositoryBase<Category, Guid, AppDbContext>(context), ICategoryRepository
{
}
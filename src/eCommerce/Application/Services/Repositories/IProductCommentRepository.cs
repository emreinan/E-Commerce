using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductCommentRepository : IAsyncRepository<ProductComment, Guid>, IRepository<ProductComment, Guid>
{
}
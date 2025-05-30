using Application.Fetaures.Categories.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Fetaures.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository,
                                     CategoryBusinessRules categoryBusinessRules) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                 include: q => q.Include(c => c.Products)
                   .ThenInclude(p => p.ProductImages)
                   .Include(c => c.Products)
                   .ThenInclude(p => p.ProductComments),
                cancellationToken: cancellationToken);

            categoryBusinessRules.CategoryShouldExistWhenSelected(category);

            foreach (var product in category!.Products)
            {
                product.DeletedDate = DateTime.UtcNow;

                foreach (var img in product.ProductImages)
                    img.DeletedDate = DateTime.UtcNow;
                
                foreach (var comment in product.ProductComments)
                    comment.DeletedDate = DateTime.UtcNow;
            }

            await categoryRepository.DeleteAsync(category, cancellationToken: cancellationToken);
        }
    }
}
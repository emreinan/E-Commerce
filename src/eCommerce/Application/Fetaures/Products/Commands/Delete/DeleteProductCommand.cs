using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Products.Rules;
using Core.Application.Pipelines.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Application.Fetaures.Products.Commands.Delete;

public class DeleteProductCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteProductCommandHandler(IProductRepository productRepository,
                                     ProductBusinessRules productBusinessRules) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.GetAsync(
                predicate: p => p.Id == request.Id,
                include: q => q
                .Include(p => p.ProductImages)
                .Include(p => p.ProductComments),
                cancellationToken: cancellationToken);

            productBusinessRules.ProductShouldExistWhenSelected(product);

            foreach (var image in product!.ProductImages)
                image.DeletedDate = DateTime.UtcNow;

            foreach (var comment in product.ProductComments)
                comment.DeletedDate = DateTime.UtcNow;

            await productRepository.DeleteAsync(product!, cancellationToken: cancellationToken);
        }
    }
}
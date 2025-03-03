using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Products.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Products.Commands.Delete;

public class DeleteProductCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteProductCommandHandler(IProductRepository productRepository,
                                     ProductBusinessRules productBusinessRules) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            productBusinessRules.ProductShouldExistWhenSelected(product);

            await productRepository.DeleteAsync(product!);
        }
    }
}
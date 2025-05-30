using Application.Fetaures.ProductImages.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.ProductImages.Commands.Delete;

public class DeleteProductImageCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteProductImageCommandHandler(
        IProductImageRepository productImageRepository,
        ProductImageBusinessRules productImageBusinessRules)
        : IRequestHandler<DeleteProductImageCommand>
    {
        public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            ProductImage? productImage = await productImageRepository.GetAsync(
                predicate: pi => pi.Id == request.Id,
                cancellationToken: cancellationToken
            );

            productImageBusinessRules.ProductImageShouldExistWhenSelected(productImage);

            await productImageBusinessRules.EnsureProductImageCanBeDeleted(productImage!, cancellationToken);

            await productImageRepository.DeleteAsync(productImage!,cancellationToken: cancellationToken);
        }
    }
}

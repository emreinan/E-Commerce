using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Stores.Rules;
using Core.Application.Pipelines.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Application.Fetaures.Stores.Commands.Delete;

public class DeleteStoreCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteStoreCommandHandler(IStoreRepository storeRepository, StoreBusinessRules storeBusinessRules) : IRequestHandler<DeleteStoreCommand>
    {
        public async Task Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            Store? store = await storeRepository.GetAsync(
                predicate: s => s.Id == request.Id,
                  include: q => q
                    .Include(s => s.Products)
                        .ThenInclude(p => p.ProductImages)
                    .Include(s => s.Products)
                        .ThenInclude(p => p.ProductComments),
                cancellationToken: cancellationToken);

            storeBusinessRules.StoreShouldExistWhenSelected(store);

            foreach (var product in store!.Products)
            {
                product.DeletedDate = DateTime.UtcNow;

                foreach (var image in product.ProductImages)
                    image.DeletedDate = DateTime.UtcNow;

                foreach (var comment in product.ProductComments)
                    comment.DeletedDate = DateTime.UtcNow;
            }

            await storeRepository.DeleteAsync(store!, cancellationToken: cancellationToken);
        }
    }
}

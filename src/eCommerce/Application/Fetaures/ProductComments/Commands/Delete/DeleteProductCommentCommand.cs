using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Core.Application.Pipelines.Transaction;
using Application.Fetaures.ProductComments.Rules;

namespace Application.Fetaures.ProductComments.Commands.Delete;

public class DeleteProductCommentCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteProductCommentCommandHandler(IProductCommentRepository productCommentRepository,
                                     ProductCommentBusinessRules productCommentBusinessRules) : IRequestHandler<DeleteProductCommentCommand>
    {
        public async Task Handle(DeleteProductCommentCommand request, CancellationToken cancellationToken)
        {
            ProductComment? productComment = await productCommentRepository.GetAsync(predicate: pc => pc.Id == request.Id, cancellationToken: cancellationToken);
            productCommentBusinessRules.ProductCommentShouldExistWhenSelected(productComment);

            await productCommentRepository.DeleteAsync(productComment!, cancellationToken: cancellationToken);
        }
    }
}
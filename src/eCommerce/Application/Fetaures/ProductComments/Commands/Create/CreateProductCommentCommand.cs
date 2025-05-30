using Application.Fetaures.ProductComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.ProductComments.Commands.Create;

public record CreateProductCommentCommand(
    Guid ProductId, 
    Guid UserId, 
    string Text, 
    byte StarCount)
    : IRequest<CreatedProductCommentResponse>, ITransactionalRequest
{
    public class CreateProductCommentCommandHandler(
    IMapper mapper,
    IProductCommentRepository productCommentRepository,
    ProductCommentBusinessRules businessRules)
    : IRequestHandler<CreateProductCommentCommand, CreatedProductCommentResponse>
    {
        public async Task<CreatedProductCommentResponse> Handle(CreateProductCommentCommand request, CancellationToken cancellationToken)
        {
            await businessRules.EnsureProductExists(request.ProductId, cancellationToken);
            await businessRules.EnsureUserExists(request.UserId, cancellationToken);
            await businessRules.EnsureUserPurchasedProduct(request.UserId, request.ProductId, cancellationToken);

            ProductComment productComment = mapper.Map<ProductComment>(request);
            await productCommentRepository.AddAsync(productComment, cancellationToken);

            return mapper.Map<CreatedProductCommentResponse>(productComment);
        }
    }
}
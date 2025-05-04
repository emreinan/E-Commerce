using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Core.Application.Pipelines.Transaction;
using Application.Fetaures.ProductComments.Rules;

namespace Application.Fetaures.ProductComments.Commands.Update;

public class UpdateProductCommentCommand : IRequest<UpdatedProductCommentResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateProductCommentRequest Request { get; set; } = default!;

    public class UpdateProductCommentCommandHandler(IMapper mapper, IProductCommentRepository productCommentRepository,
                                     ProductCommentBusinessRules productCommentBusinessRules) : IRequestHandler<UpdateProductCommentCommand, UpdatedProductCommentResponse>
    {
        public async Task<UpdatedProductCommentResponse> Handle(UpdateProductCommentCommand request, CancellationToken cancellationToken)
        {
            ProductComment? productComment = await productCommentRepository.GetAsync(predicate: pc => pc.Id == request.Id, cancellationToken: cancellationToken);
            productCommentBusinessRules.ProductCommentShouldExistWhenSelected(productComment);
            productComment = mapper.Map(request.Request, productComment);

            await productCommentRepository.UpdateAsync(productComment!, cancellationToken);

            UpdatedProductCommentResponse response = mapper.Map<UpdatedProductCommentResponse>(productComment);
            return response;
        }
    }
}
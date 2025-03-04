using Application.Features.ProductComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductComments.Queries.GetById;

public class GetByIdProductCommentQuery : IRequest<GetByIdProductCommentResponse>
{
    public Guid Id { get; set; }

    public class GetByIdProductCommentQueryHandler(IMapper mapper,
                                                   IProductCommentRepository productCommentRepository,
                                                   ProductCommentBusinessRules productCommentBusinessRules) : IRequestHandler<GetByIdProductCommentQuery, GetByIdProductCommentResponse>
    {
        public async Task<GetByIdProductCommentResponse> Handle(GetByIdProductCommentQuery request, CancellationToken cancellationToken)
        {
            ProductComment? productComment = await productCommentRepository.GetAsync(predicate: pc => pc.Id == request.Id, cancellationToken: cancellationToken);
            productCommentBusinessRules.ProductCommentShouldExistWhenSelected(productComment);

            GetByIdProductCommentResponse response = mapper.Map<GetByIdProductCommentResponse>(productComment);
            return response;
        }
    }
}
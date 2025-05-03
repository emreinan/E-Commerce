using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.ProductComments.Queries.GetList;

public class GetListProductCommentQuery : IRequest<IEnumerable<GetListProductCommentListItemDto>>
{
    public class GetListProductCommentQueryHandler(IProductCommentRepository productCommentRepository, IMapper mapper) : IRequestHandler<GetListProductCommentQuery, IEnumerable<GetListProductCommentListItemDto>>
    {
        public async Task<IEnumerable<GetListProductCommentListItemDto>> Handle(GetListProductCommentQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductComment> productComments = await productCommentRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<IEnumerable<GetListProductCommentListItemDto>>(productComments);
            return response;
        }
    }
}
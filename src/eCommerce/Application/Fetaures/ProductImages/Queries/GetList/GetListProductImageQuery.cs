using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.ProductImages.Queries.GetList;

public class GetListProductImageQuery : IRequest<IEnumerable<GetListProductImageListItemDto>>
{
    public class GetListProductImageQueryHandler(IProductImageRepository productImageRepository, IMapper mapper) : IRequestHandler<GetListProductImageQuery, IEnumerable<GetListProductImageListItemDto>>
    {
        public async Task<IEnumerable<GetListProductImageListItemDto>> Handle(GetListProductImageQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductImage> productImages = await productImageRepository.GetListAsync(
                cancellationToken: cancellationToken);

            IEnumerable<GetListProductImageListItemDto> response = mapper.Map<IEnumerable<GetListProductImageListItemDto>>(productImages);
            return response;
        }
    }
}
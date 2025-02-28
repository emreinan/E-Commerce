using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<IEnumerable<GetListCategoryListItemDto>>
{

    public class GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetListCategoryQuery, IEnumerable<GetListCategoryListItemDto>>
    {
        public async Task<IEnumerable<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Category> categories = await categoryRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<IEnumerable<GetListCategoryListItemDto>>(categories);
            return response;
        }
    }
}
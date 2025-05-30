using Application.Fetaures.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Categories.Queries.GetById;

public class GetByIdCategoryQuery : IRequest<GetByIdCategoryResponse>
{
    public Guid Id { get; set; }

    public class GetByIdCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                             CategoryBusinessRules categoryBusinessRules) : IRequestHandler<GetByIdCategoryQuery, GetByIdCategoryResponse>
    {
        public async Task<GetByIdCategoryResponse> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            categoryBusinessRules.CategoryShouldExistWhenSelected(category);

            GetByIdCategoryResponse response = mapper.Map<GetByIdCategoryResponse>(category);
            return response;
        }
    }
}
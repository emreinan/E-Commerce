using Application.Fetaures.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Application.Fetaures.Categories.Commands.Create;

namespace Application.Fetaures.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<CreatedCategoryResponse>, ITransactionalRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public class CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                     CategoryBusinessRules categoryBusinessRules) : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryBusinessRules.CategoryNameBeUnique(request.Name, cancellationToken);
            Category category = mapper.Map<Category>(request);

            await categoryRepository.AddAsync(category);

            CreatedCategoryResponse response = mapper.Map<CreatedCategoryResponse>(category);
            return response;
        }
    }
}
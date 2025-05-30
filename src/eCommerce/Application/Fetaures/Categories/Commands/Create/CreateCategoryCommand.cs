using Application.Fetaures.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Categories.Commands.Create;

public record CreateCategoryCommand(string? Description, string Name) : IRequest<CreatedCategoryResponse>, ITransactionalRequest
{
    public class CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                     CategoryBusinessRules categoryBusinessRules) : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryBusinessRules.CategoryNameBeUnique(request.Name.Trim(), cancellationToken);
            Category category = mapper.Map<Category>(request);

            await categoryRepository.AddAsync(category, cancellationToken);

            CreatedCategoryResponse response = mapper.Map<CreatedCategoryResponse>(category);
            return response;
        }
    }
}
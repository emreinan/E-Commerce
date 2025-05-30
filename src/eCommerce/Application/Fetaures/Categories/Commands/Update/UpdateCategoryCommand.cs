using Application.Fetaures.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Fetaures.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<UpdatedCategoryResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateCategoryRequest Request { get; set; } = default!;

    public class UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                     CategoryBusinessRules categoryBusinessRules) : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryResponse>
    {
        public async Task<UpdatedCategoryResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.GetAsync(predicate: c => c.Id == command.Id, cancellationToken: cancellationToken);
            categoryBusinessRules.CategoryShouldExistWhenSelected(category);

            await categoryBusinessRules.CategoryNameBeUnique(command.Request.Name.Trim(), cancellationToken);

            category = mapper.Map(command.Request, category);

            await categoryRepository.UpdateAsync(category!, cancellationToken);

            UpdatedCategoryResponse response = mapper.Map<UpdatedCategoryResponse>(category);
            return response;
        }
    }
}
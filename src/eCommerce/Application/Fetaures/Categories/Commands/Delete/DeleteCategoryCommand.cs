using Application.Fetaures.Categories.Constants;
using Application.Fetaures.Categories.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Fetaures.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository,
                                     CategoryBusinessRules categoryBusinessRules) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            categoryBusinessRules.CategoryShouldExistWhenSelected(category);

            await categoryRepository.DeleteAsync(category!);
        }
    }
}
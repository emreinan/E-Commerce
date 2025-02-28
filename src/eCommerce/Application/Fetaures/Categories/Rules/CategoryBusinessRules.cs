using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Categories.Rules;

public class CategoryBusinessRules(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void CategoryShouldExistWhenSelected(Category? category)
    {
        if (category is null)
            throw new BusinessException(CategoriesBusinessMessages.CategoryNotExists);
    }

    public async Task CategoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        CategoryShouldExistWhenSelected(category);
    }
    public async Task CategoryNameBeUnique(string name, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetAsync(
            predicate: c => c.Name == name,
            cancellationToken: cancellationToken
        );
        if (category is not null)
            throw new BusinessException(CategoriesBusinessMessages.CategoryNameAlreadyExists);
    }
}
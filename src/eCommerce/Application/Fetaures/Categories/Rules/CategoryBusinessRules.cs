using Application.Fetaures.Categories.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Categories.Rules;

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
        string normalizedName = name.Trim().ToLowerInvariant();
        Category? category = await categoryRepository.GetAsync(
            predicate: c => c.Name.ToLower() == normalizedName,
            cancellationToken: cancellationToken
        );
        if (category is not null)
            throw new BusinessException(CategoriesBusinessMessages.CategoryNameAlreadyExists);
    }

}
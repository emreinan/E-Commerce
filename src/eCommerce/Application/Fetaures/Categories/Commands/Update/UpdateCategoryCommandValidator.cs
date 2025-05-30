using FluentValidation;

namespace Application.Fetaures.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Name)
           .NotEmpty()
           .MaximumLength(100)
           .Matches("^[a-zA-Z������������\\-\\s]*$")
           .WithMessage("Kategori ad� yaln�zca harf (T�rk�e dahil), bo�luk ve tire i�erebilir.");

        RuleFor(c => c.Description).MaximumLength(200);
    }
}
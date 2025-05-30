using FluentValidation;

namespace Application.Fetaures.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Name)
           .NotEmpty()
           .MaximumLength(100)
           .Matches("^[a-zA-ZçÇðÐýÝöÖþÞüÜ\\-\\s]*$")
           .WithMessage("Kategori adý yalnýzca harf (Türkçe dahil), boþluk ve tire içerebilir.");

        RuleFor(c => c.Description).MaximumLength(200);
    }
}
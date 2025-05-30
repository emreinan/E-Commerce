using FluentValidation;

namespace Application.Fetaures.Categories.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-ZçÇðÐýÝöÖþÞüÜ\\-\\s]*$")
            .WithMessage("Kategori adý yalnýzca harf (Türkçe dahil), boþluk ve tire içerebilir.");

        RuleFor(c => c.Description).MaximumLength(200);
    }
}

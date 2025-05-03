using FluentValidation;

namespace Application.Fetaures.ProductImages.Commands.Create;

public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty().MaximumLength(500);
    }
}
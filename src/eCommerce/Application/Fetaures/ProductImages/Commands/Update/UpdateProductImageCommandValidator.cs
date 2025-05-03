using FluentValidation;

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageRequest>
{
    public UpdateProductImageCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty().MaximumLength(500);
    }
}
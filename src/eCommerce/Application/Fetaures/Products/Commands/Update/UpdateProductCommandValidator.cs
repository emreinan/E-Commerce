using FluentValidation;

namespace Application.Fetaures.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.StoreId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Name)
             .NotEmpty()
             .MaximumLength(200).WithMessage("Product name must be at most 200 characters.");
        RuleFor(c => c.Price)
            .GreaterThan(0).WithMessage("Fiyat sýfýrdan büyük olmalý!");

        RuleFor(c => c.StockAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarý negatif olamaz!");
    }
}
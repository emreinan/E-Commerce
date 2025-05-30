using FluentValidation;

namespace Application.Fetaures.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.StoreId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Name)
             .NotEmpty()
             .MaximumLength(200).WithMessage("Product name must be at most 200 characters.");
        RuleFor(c => c.Details)
             .MaximumLength(1000).WithMessage("Details must be at most 1000 characters.");
        RuleFor(c => c.Price)
            .GreaterThan(0).WithMessage("Fiyat sýfýrdan büyük olmalý!");

        RuleFor(c => c.StockAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarý negatif olamaz!");

        RuleFor(c => c.Name)
           .Matches("^[a-zA-Z0-9ðüþöçÝÐÜÞÖÇ ]*$")
           .WithMessage("Product name contains invalid characters.");

    }
}
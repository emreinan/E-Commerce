using FluentValidation;

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageRequest>
{
    public UpdateProductImageCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.File)
            .NotNull().WithMessage("Bir dosya yüklemelisiniz.")
            .Must(file => file.Length > 0).WithMessage("Dosya boþ olamaz.")
            .Must(file => file.Length <= 5 * 1024 * 1024)
            .WithMessage("Dosya boyutu 5 MB'dan büyük olamaz.");
    }
}
using FluentValidation;

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageRequest>
{
    public UpdateProductImageCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.File)
            .NotNull().WithMessage("Bir dosya y�klemelisiniz.")
            .Must(file => file.Length > 0).WithMessage("Dosya bo� olamaz.")
            .Must(file => file.Length <= 5 * 1024 * 1024)
            .WithMessage("Dosya boyutu 5 MB'dan b�y�k olamaz.");
    }
}
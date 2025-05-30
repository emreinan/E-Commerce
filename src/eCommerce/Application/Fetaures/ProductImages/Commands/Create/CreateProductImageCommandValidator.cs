using FluentValidation;

namespace Application.Fetaures.ProductImages.Commands.Create;

public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(x => x.Files)
            .NotEmpty().WithMessage("En az bir foto�raf y�klenmelidir.")
            .Must(f => f.Count <= 3).WithMessage("En fazla 3 foto�raf y�klenebilir.");

        RuleForEach(x => x.Files)
            .Must(file => file.Length <= 5 * 1024 * 1024) // 5MB s�n�r�
            .WithMessage("Foto�raf boyutu en fazla 5MB olabilir.");

        RuleFor(c => c.MainImageIndex)
            .GreaterThanOrEqualTo(0)
            .Must((cmd, index) => index < cmd.Files.Count)
            .WithMessage("Main image index is out of bounds.");
    }
}

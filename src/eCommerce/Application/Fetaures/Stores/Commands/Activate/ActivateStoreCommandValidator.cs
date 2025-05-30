using FluentValidation;

public class ActivateStoreCommandValidator : AbstractValidator<ActivateStoreCommand>
{
    public ActivateStoreCommandValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty();
    }
}

using FluentValidation;

public class VerifyStoreCommandValidator : AbstractValidator<VerifyStoreCommand>
{
    public VerifyStoreCommandValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}


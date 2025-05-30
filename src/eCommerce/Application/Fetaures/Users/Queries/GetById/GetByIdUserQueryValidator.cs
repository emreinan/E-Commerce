using FluentValidation;

namespace Application.Fetaures.Users.Queries.GetById;

public class GetByIdUserQueryValidator : AbstractValidator<GetByIdUserQuery>
{
    public GetByIdUserQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

using AmigosConCola.Core.Repositories;
using FluentValidation;

namespace AmigosConCola.Core.Validations;

public class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PerPage).GreaterThanOrEqualTo(10);
    }
}
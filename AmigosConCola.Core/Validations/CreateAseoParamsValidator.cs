using AmigosConCola.Core.Repositories;
using FluentValidation;

namespace AmigosConCola.Core.Validations;

public class CreateAseoParamsValidator : AbstractValidator<CreateAseoParams>
{
    public CreateAseoParamsValidator()
    {
        RuleFor(x => x.Tipo).NotEmpty();
    }
}
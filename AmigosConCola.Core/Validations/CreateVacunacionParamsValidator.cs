using AmigosConCola.Core.Repositories;
using FluentValidation;

namespace AmigosConCola.Core.Validations;

public sealed class CreateVacunacionParamsValidator : AbstractValidator<CreateVacunacionParams>
{
    public CreateVacunacionParamsValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
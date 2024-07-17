using AmigosConCola.Core.Repositories;
using FluentValidation;

namespace AmigosConCola.Core.Validations;

public class CreatePesoParamsValidator: AbstractValidator<CreatePesoParams>
{
    public CreatePesoParamsValidator()
    {
        RuleFor(x => x.PesoActual).NotEmpty();
        RuleFor(x => x.Fecha).NotEmpty();
    }
}
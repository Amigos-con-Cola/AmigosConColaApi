using AmigosConCola.Core.Repositories;
using FluentValidation;

namespace AmigosConCola.Core.Validations;


public sealed class CreateDesparasitacionParamsValidator : AbstractValidator<CreateDesparasitacionParams>
{
    public CreateDesparasitacionParamsValidator()
    {
        RuleFor(x => x.Tipo)
          .Must(x => string.Equals(x.ToLower(), "interno") || string.Equals(x.ToLower(), "externo"))
          .WithMessage("Tipo must be one of 'interno' or 'externo'");

        RuleFor(x => x.Producto).NotEmpty();
        RuleFor(x => x.Formato).NotEmpty();
        RuleFor(x => x.Peso).GreaterThan(0);
    }
}

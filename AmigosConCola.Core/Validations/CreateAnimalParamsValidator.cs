using AmigosConCola.Core.Repositories;
using FluentValidation;

namespace AmigosConCola.Core.Validations;

public sealed class CreateAnimalParamsValidator : AbstractValidator<CreateAnimalParams>
{
    public CreateAnimalParamsValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Age).GreaterThan(0).LessThan(20);
    }
}
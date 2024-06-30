using AmigosConCola.Core.Extensions;
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public sealed class CreateDesparasitacionUseCase
{
    private readonly IDesparasitacionRepository _desparasitaciones;

    public CreateDesparasitacionUseCase(IDesparasitacionRepository desparasitaciones)
    {
        _desparasitaciones = desparasitaciones;
    }

    public async Task<ErrorOr<Desparasitacion>> Invoke(CreateDesparasitacionParams parameters)
    {
        var validator = new CreateDesparasitacionParamsValidator();
        var validationResult = validator.Validate(parameters);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        return await _desparasitaciones.Create(parameters);
    }
}


using AmigosConCola.Core.Extensions;
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class CreateVacunacionUseCase
{
    private readonly IVacunacionRepository _vacunaciones;

    public CreateVacunacionUseCase(IVacunacionRepository vacunaciones)
    {
        _vacunaciones = vacunaciones;
    }

    public async Task<ErrorOr<Vacunacion>> Invoke(CreateVacunacionParams parameters)
    {
        var validator = new CreateVacunacionParamsValidator();
        var result = await validator.ValidateAsync(parameters);

        if (!result.IsValid)
        {
            return result.ToErrors();
        }

        return await _vacunaciones.Create(parameters);
    }
}
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class FindAllVacunacionesUseCase
{
    private readonly IVacunacionRepository _vacunaciones;

    public FindAllVacunacionesUseCase(IVacunacionRepository vacunaciones)
    {
        _vacunaciones = vacunaciones;
    }

    public async Task<ErrorOr<IEnumerable<Vacunacion>>> Invoke(int animalId)
    {
        return await _vacunaciones.FindAll(animalId);
    }
}
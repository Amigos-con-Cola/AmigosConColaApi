using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;

namespace AmigosConCola.Core.UseCases;


public class FindAllDesparasitacionesUseCase
{
    private readonly IDesparasitacionRepository _desparasitaciones;

    public FindAllDesparasitacionesUseCase(IDesparasitacionRepository desparasitaciones)
    {
        _desparasitaciones = desparasitaciones;
    }

    public async Task<IEnumerable<Desparasitacion>> Invoke(int idAnimal)
    {
        return await _desparasitaciones.FindAll(idAnimal);
    }
}

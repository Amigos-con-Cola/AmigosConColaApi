using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;

namespace AmigosConCola.Core.UseCases;

public class FindAllAseosUseCase
{
    private readonly IAseosRepository _aseos;

    public FindAllAseosUseCase(IAseosRepository aseos)
    {
        _aseos = aseos;
    }

    public Task<IEnumerable<Aseo>> Invoke(int idAnimal)
    {
        // TODO: Validate the animal id.
        return _aseos.FindAll(idAnimal);
    }
}
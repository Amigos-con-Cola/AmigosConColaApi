using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;

namespace AmigosConCola.Core.UseCases;

public class FindAllPesosUseCase
{
    private readonly IPesosRepository _pesos;

    public FindAllPesosUseCase(IPesosRepository pesos)
    {
        _pesos = pesos;
    }

    public Task<IEnumerable<Peso>> Invoke(int idAnimal)
    {
        return _pesos.FindAll(idAnimal);
    }
}

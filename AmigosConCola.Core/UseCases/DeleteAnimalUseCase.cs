using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class DeleteAnimalUseCase
{
    private readonly IAnimalRepository _animals;

    public DeleteAnimalUseCase(
        IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<bool>> Invoke(int id)
    {
        return await _animals.Delete(id);
    }
}
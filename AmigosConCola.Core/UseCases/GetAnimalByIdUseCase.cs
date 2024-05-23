using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class GetAnimalByIdUseCase
{
    private readonly IAnimalRepository _animals;

    public GetAnimalByIdUseCase(IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<Animal>> Invoke(int id)
    {
        return await _animals.GetById(id);
    }
}
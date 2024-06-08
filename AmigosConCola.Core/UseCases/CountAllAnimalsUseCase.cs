using AmigosConCola.Core.Repositories;

namespace AmigosConCola.Core.UseCases;

public class CountAllAnimalsUseCase
{
    private readonly IAnimalRepository _animals;

    public CountAllAnimalsUseCase(IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<int> Invoke()
    {
        return await _animals.CountAll();
    }
}
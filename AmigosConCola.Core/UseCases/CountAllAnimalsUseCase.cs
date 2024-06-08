using AmigosConCola.Core.Repositories;

namespace AmigosConCola.Core.UseCases;

public class CountAllAnimalsUseCase
{
    private readonly IAnimalRepository _animals;

    public CountAllAnimalsUseCase(IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<int> Invoke(GetAllAnimalsFilters filters)
    {
        return await _animals.CountAll(filters);
    }
}
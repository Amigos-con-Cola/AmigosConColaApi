using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class UpdateAnimalImageUrlUseCase
{
    private readonly IAnimalRepository _animals;

    public UpdateAnimalImageUrlUseCase(IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<Animal>> Invoke(int id, string newUrl)
    {
        return await _animals.UpdateImageUrl(id, newUrl);
    }
}
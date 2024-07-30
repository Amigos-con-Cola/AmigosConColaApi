using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class UpdateAnimalUseCase
{
    private readonly IAnimalRepository _animals;

    public UpdateAnimalUseCase(IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<Animal>> Invoke(int id, UpdateAnimalParams parameters)
    {
        if (parameters.Species is not null && !Enum.TryParse(parameters.Species, true, out AnimalSpecies _))
            return Error.Validation(description: $"Invalid animal species: {parameters.Species}");

        if (parameters.Gender is not null && !Enum.TryParse(parameters.Gender, true, out AnimalGender _))
            return Error.Validation(description: $"Invalid animal gender: {parameters.Gender}");

        return await _animals.Update(id, parameters);
    }
}
using AmigosConCola.Core.Extensions;
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public sealed class GetAllAnimalsUseCase
{
    private readonly IAnimalRepository _animals;

    public GetAllAnimalsUseCase(
        IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<IEnumerable<Animal>>> Invoke(PaginationParams parameters, GetAllAnimalsFilters filters)
    {
        var validator = new PaginationParamsValidator();
        var validationResult = validator.Validate(parameters);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        return await _animals.GetAll(parameters, filters);
    }
}
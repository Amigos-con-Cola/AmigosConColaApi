using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public sealed class CreateAnimalUseCase
{
    private readonly IAnimalRepository _animals;

    public CreateAnimalUseCase(
        IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<Animal>> Invoke(CreateAnimalParams parameters)
    {
        var validator = new CreateAnimalParamsValidator();
        var validationResult = validator.Validate(parameters);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ConvertAll(
                error => Error.Validation(
                    error.PropertyName,
                    error.ErrorMessage));
            return errors;
        }

        return await _animals.Create(parameters);
    }
}
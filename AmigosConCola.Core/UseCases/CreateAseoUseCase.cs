using AmigosConCola.Core.Extensions;
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public sealed class CreateAseoUseCase
{
    private readonly IAseosRepository _aseos;

    public CreateAseoUseCase(IAseosRepository aseos)
    {
        _aseos = aseos;
    }

    public async Task<ErrorOr<Aseo>> Invoke(CreateAseoParams parameters)
    {
        var validator = new CreateAseoParamsValidator();
        var result = await validator.ValidateAsync(parameters);

        if (!result.IsValid)
        {
            return result.ToErrors();
        }

        return await _aseos.Create(parameters);
    }
}

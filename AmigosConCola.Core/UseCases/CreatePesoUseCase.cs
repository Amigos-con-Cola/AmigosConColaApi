using AmigosConCola.Core.Extensions;
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public sealed class CreatePesoUseCase
{
    private readonly IPesosRepository _pesos;
    
    public CreatePesoUseCase(IPesosRepository pesos)
    {
        _pesos = pesos;
    }
    
    public async Task<ErrorOr<Peso>> Invoke(CreatePesoParams parameters)
    {
        var validator = new CreatePesoParamsValidator();
        var result = await validator.ValidateAsync(parameters);
        
        if (!result.IsValid)
        {
            return result.ToErrors();
        }
        
        return await _pesos.Create(parameters);
    }
    
}
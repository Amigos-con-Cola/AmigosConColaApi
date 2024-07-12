using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animales")]
public class AseoController : BaseApiController
{
    private readonly CreateAseoUseCase _createAseo;

    public AseoController(CreateAseoUseCase createAseo)
    {
        _createAseo = createAseo;
    }

    [HttpPost("{idAnimal:int}/aseos")]
    public async Task<IActionResult> Store(
        int idAnimal,
        CreateAseoRequest request)
    {
        var createAseoParams = new CreateAseoParams
        {
            IdAnimal = idAnimal,
            Tipo = request.Tipo,
            Fecha = request.Fecha
        };


        var result = await _createAseo.Invoke(createAseoParams);

        if (result.IsError && result.FirstError.Type == ErrorType.Validation)
        {
            return ValidationErrors(result.Errors);
        }

        if (result.IsError)
        {
            return Problem(
                statusCode: 400,
                detail: result.FirstError.Description);
        }

        var response = new AseoResponse
        {
            Id = result.Value.Id,
            IdAnimal = result.Value.IdAnimal,
            Tipo = result.Value.Tipo,
            Fecha = result.Value.Fecha
        };

        return Created(
            $"/api/animales/{idAnimal}/aseos/{result.Value.Id}",
            response);
    }
}
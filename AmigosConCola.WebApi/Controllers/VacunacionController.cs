using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animales/")]
public class VacunacionController : BaseApiController
{
    private readonly CreateVacunacionUseCase _createVacunacion;

    public VacunacionController(
        CreateVacunacionUseCase createVacunacion)
    {
        _createVacunacion = createVacunacion;
    }

    [HttpPost("{animalId:int}/vacunaciones")]
    public async Task<IActionResult> Store(
        int animalId,
        [FromBody]
        CreateVacunacionRequest request)
    {
        var parameters = new CreateVacunacionParams
        {
            IdAnimal = animalId,
            Name = request.Name,
            Date = request.Date
        };

        var result = await _createVacunacion.Invoke(parameters);

        if (result.IsError && result.FirstError.Type == ErrorType.NotFound)
        {
            return Problem(
                result.FirstError.Description,
                statusCode: 404);
        }

        if (result.IsError)
        {
            return ValidationErrors(result.Errors);
        }

        return Created(
            $"/api/animals/{animalId}/vacunaciones/{result.Value.Id}",
            VacunacionResponse.FromDomain(result.Value));
    }
}
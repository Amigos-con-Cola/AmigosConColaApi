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
    private readonly FindAllVacunacionesUseCase _findAllVacunaciones;

    public VacunacionController(
        CreateVacunacionUseCase createVacunacion,
        FindAllVacunacionesUseCase findAllVacunaciones)
    {
        _createVacunacion = createVacunacion;
        _findAllVacunaciones = findAllVacunaciones;
    }

    [HttpGet("{animalId:int}/vacunaciones")]
    public async Task<IActionResult> Index(int animalId)
    {
        var result = await _findAllVacunaciones.Invoke(animalId);

        if (result.IsError && result.FirstError.Type == ErrorType.NotFound)
        {
            return NotFound(result.FirstError.Description);
        }

        return Ok(result.Value);
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
            Date = request.Date,
            ExamenPrevio = request.ExamenPrevio
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
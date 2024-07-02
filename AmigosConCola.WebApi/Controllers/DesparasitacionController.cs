using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animals/")]
public sealed class DesparasitacionController : BaseApiController
{
    private readonly ILogger<DesparasitacionController> _logger;
    private readonly CreateDesparasitacionUseCase _createDesparasitacion;

    public DesparasitacionController(
            ILogger<DesparasitacionController> logger,
            CreateDesparasitacionUseCase createDesparasitacion)
    {
        _logger = logger;
        _createDesparasitacion = createDesparasitacion;
    }

    [HttpPost("{animalId:int}/desparasitaciones")]
    public async Task<IActionResult> Store(int animalId, [FromBody] CreateDesparasitacionRequest request)
    {
        var createDesparasitacionParams = new CreateDesparasitacionParams(
            IdAnimal: animalId,
            Tipo: request.Tipo,
            Fecha: request.Fecha,
            Producto: request.Producto,
            Peso: request.Peso,
            Formato: request.Formato);

        _logger.LogInformation(
            "Creating desparasitacion for animal {AnimalId} with params {@CreateDesparasitacionParams}",
            animalId,
            createDesparasitacionParams);

        var result = await _createDesparasitacion.Invoke(createDesparasitacionParams);

        if (result.IsError)
        {
            return Problem(
                detail: result.FirstError.Description,
                statusCode: 400);
        }

        var response = new DesparasitacionResponse
        {
            Id = result.Value.Id,
            IdAnimal = result.Value.IdAnimal,
            Tipo = result.Value.Tipo,
            Fecha = result.Value.Fecha,
            Producto = result.Value.Producto,
            Peso = result.Value.Peso,
            Formato = result.Value.Formato
        };

        return Created(
                $"/api/animals/{animalId}/desparasitaciones/{response.Id}",
                response);
    }
}

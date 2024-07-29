using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animals/")]
public sealed class DesparasitacionController : BaseApiController
{
    private readonly ILogger<DesparasitacionController> _logger;
    private readonly CreateDesparasitacionUseCase _createDesparasitacion;
    private readonly FindAllDesparasitacionesUseCase _findAllDesparasitaciones;

    public DesparasitacionController(
            ILogger<DesparasitacionController> logger,
            CreateDesparasitacionUseCase createDesparasitacion,
            FindAllDesparasitacionesUseCase findAllDesparasitaciones)
    {
        _logger = logger;
        _createDesparasitacion = createDesparasitacion;
        _findAllDesparasitaciones = findAllDesparasitaciones;
    }

    [HttpGet("{animalId:int}/desparasitaciones")]
    public async Task<IActionResult> Index(int animalId)
    {
        var result = await _findAllDesparasitaciones.Invoke(animalId);
        var response = result.Select(x => new DesparasitacionResponse
        {
            Id = x.Id,
            IdAnimal = x.IdAnimal,
            Tipo = x.Tipo,
            Fecha = x.Fecha,
            Producto = x.Producto,
            Peso = x.Peso,
            Formato = x.Formato
        });
        return Ok(response);
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

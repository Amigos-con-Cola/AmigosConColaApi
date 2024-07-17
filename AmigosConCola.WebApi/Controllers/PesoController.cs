using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animales")]
public class PesoController: BaseApiController
{
    private readonly CreatePesoUseCase _createPeso;
    private readonly IMapper _mapper;
    
    public PesoController(CreatePesoUseCase createPeso, IMapper mapper)
    {
        _createPeso = createPeso;
        _mapper = mapper;
    }
    
    [HttpPost("{idAnimal:int}/pesos")]
    public async Task<IActionResult> Store(
        int idAnimal,
        CreatePesoRequest request)
    {
        var createPesoParams = new CreatePesoParams
        {
            IdAnimal = idAnimal,
            PesoActual = request.PesoActual,
            Fecha = request.Fecha
        };

        var result = await _createPeso.Invoke(createPesoParams);

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

        var response = _mapper.Map<PesoResponse>(result.Value);

        return Created(
            $"/api/animales/{idAnimal}/pesos/{result.Value.Id}",
            response);
    }
    
}
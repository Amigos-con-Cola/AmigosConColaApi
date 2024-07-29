using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Throw;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animales/{idAnimal:int}/pesos")]
public class PesoController : BaseApiController
{
    private readonly CreatePesoUseCase _createPeso;
    private readonly FindAllPesosUseCase _findAllPesos;
    private readonly ILogger<PesoController> _logger;
    private readonly IMapper _mapper;

    public PesoController(
        ILogger<PesoController> logger,
        CreatePesoUseCase createPeso,
        FindAllPesosUseCase findAllPesos,
        IMapper mapper)
    {
        _logger = logger;
        _createPeso = createPeso;
        _findAllPesos = findAllPesos;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int idAnimal)
    {
        var result = await _findAllPesos.Invoke(idAnimal);
        var response = result.Select(x => _mapper.Map<PesoResponse>(x));
        return Ok(response);
    }

    [HttpPost]
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

        if (result is { IsError: true, FirstError.Type: ErrorType.Validation })
            return ValidationErrors(result.Errors);

        result.IsError
            .Throw(() => new Exception(result.FirstError.Description))
            .IfTrue();

        var response = _mapper.Map<PesoResponse>(result.Value);

        return Created(
            $"/api/animales/{idAnimal}/pesos/{result.Value.Id}",
            response);
    }
}
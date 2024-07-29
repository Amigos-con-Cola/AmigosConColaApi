using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animales")]
public class AseoController : BaseApiController
{
    private readonly CreateAseoUseCase _createAseo;
    private readonly FindAllAseosUseCase _findAllAseos;
    private readonly IMapper _mapper;

    public AseoController(
        CreateAseoUseCase createAseo,
        IMapper mapper,
        FindAllAseosUseCase findAllAseos)
    {
        _createAseo = createAseo;
        _mapper = mapper;
        _findAllAseos = findAllAseos;
    }

    [HttpGet("{idAnimal:int}/aseos")]
    public async Task<IActionResult> Index(int idAnimal)
    {
        var result = await _findAllAseos.Invoke(idAnimal);
        var response = result.Select(x => _mapper.Map<AseoResponse>(x));
        return Ok(response);
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

        var response = _mapper.Map<AseoResponse>(result.Value);

        return Created(
            $"/api/animales/{idAnimal}/aseos/{result.Value.Id}",
            response);
    }
}
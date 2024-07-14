using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animales")]
public class AseoController : BaseApiController
{
    private readonly CreateAseoUseCase _createAseo;
    private readonly IMapper _mapper;

    public AseoController(CreateAseoUseCase createAseo, IMapper mapper)
    {
        _createAseo = createAseo;
        _mapper = mapper;
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
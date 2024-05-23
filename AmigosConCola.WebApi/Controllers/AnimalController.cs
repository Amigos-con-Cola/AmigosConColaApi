using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animals/")]
public class AnimalController : BaseApiController
{
    private readonly CreateAnimalUseCase _createAnimal;
    private readonly GetAllAnimalsUseCase _getAllAnimals;
    private readonly GetAnimalByIdUseCase _getAnimalById;
    private readonly ILogger<AnimalController> _logger;

    public AnimalController(
        ILogger<AnimalController> logger,
        CreateAnimalUseCase createAnimal,
        GetAllAnimalsUseCase getAllAnimals,
        GetAnimalByIdUseCase getAnimalById)
    {
        _logger = logger;
        _createAnimal = createAnimal;
        _getAllAnimals = getAllAnimals;
        _getAnimalById = getAnimalById;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        [FromQuery]
        int? page,
        [FromQuery]
        int? perPage)
    {
        var result = await _getAllAnimals.Invoke(new PaginationParams
        {
            Page = page ?? 1,
            PerPage = perPage ?? 10
        });

        if (result.IsError)
        {
            return ValidationErrors(result.Errors);
        }

        return Ok(result.Value.Select(AnimalResponse.FromDomain));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getAnimalById.Invoke(id);

        if (result.IsError)
        {
            if (result.Errors.Count == 1 && result.FirstError.Type == ErrorType.NotFound)
            {
                return NotFound(result.FirstError.Description);
            }

            return ValidationErrors(result.Errors);
        }

        return Ok(AnimalResponse.FromDomain(result.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Store(
        [FromBody]
        CreateAnimalRequest request)
    {
        AnimalSpecies species;
        AnimalGender gender;

        if (!Enum.TryParse(request.Species, true, out species))
            return Problem("Invalid species", statusCode: 400);

        if (!Enum.TryParse(request.Gender, true, out gender))
            return Problem("Invalid gender", statusCode: 400);

        var result = await _createAnimal.Invoke(new CreateAnimalParams
        {
            Name = request.Name,
            Age = request.Age,
            Gender = gender,
            Species = species,
            ImageUrl = request.ImageUrl,
            Location = request.Location,
            Code = request.Code,
            Story = request.Story,
            Weight = request.Weight
        });

        if (result.IsError)
        {
            return ValidationErrors(result.Errors);
        }

        return Created(
            "/animals/" + result.Value.Id,
            AnimalResponse.FromDomain(result.Value));
    }
}
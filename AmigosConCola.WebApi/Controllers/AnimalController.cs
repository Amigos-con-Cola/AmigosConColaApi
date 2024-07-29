using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/animals/")]
public class AnimalController : BaseApiController
{
    private readonly CountAllAnimalsUseCase _countAllAnimals;
    private readonly CreateAnimalUseCase _createAnimal;
    private readonly DeleteAnimalUseCase _deleteAnimal;
    private readonly IWebHostEnvironment _environment;
    private readonly GetAllAnimalsUseCase _getAllAnimals;
    private readonly GetAnimalByIdUseCase _getAnimalById;
    private readonly ILogger<AnimalController> _logger;

    public AnimalController(
        ILogger<AnimalController> logger,
        IWebHostEnvironment environment,
        CreateAnimalUseCase createAnimal,
        GetAllAnimalsUseCase getAllAnimals,
        CountAllAnimalsUseCase countAllAnimals,
        GetAnimalByIdUseCase getAnimalById,
        DeleteAnimalUseCase deleteAnimal)
    {
        _logger = logger;
        _environment = environment;
        _createAnimal = createAnimal;
        _getAllAnimals = getAllAnimals;
        _getAnimalById = getAnimalById;
        _countAllAnimals = countAllAnimals;
        _deleteAnimal = deleteAnimal;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        [FromQuery]
        int? page,
        [FromQuery]
        int? perPage,
        [FromQuery]
        string? species,
        [FromQuery]
        string? name)
    {
        GetAllAnimalsFilters filters = new();

        if (species is not null)
        {
            if (Enum.TryParse(species, out AnimalSpecies speciesFilter))
                filters.Species = speciesFilter;
            else
                return Problem(statusCode: 400, detail: $"Invalid animal species: {species}");
        }

        filters.Name = name;

        var paginationParams = new PaginationParams
        {
            Page = page ?? 1,
            PerPage = perPage ?? 10
        };

        var result = await _getAllAnimals.Invoke(paginationParams, filters);

        if (result.IsError) return ValidationErrors(result.Errors);

        var count = await _countAllAnimals.Invoke(filters);

        var responseData = result.Value.Select(AnimalResponse.FromDomain);
        var response = new PaginatedDataResponse<AnimalResponse>
        {
            Data = responseData,
            NextPage = paginationParams.Page + 1,
            TotalItems = count,
            TotalPages = (int)Math.Ceiling((float)count / paginationParams.PerPage)
        };

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getAnimalById.Invoke(id);

        if (result.IsError)
        {
            if (result.Errors.Count == 1 && result.FirstError.Type == ErrorType.NotFound)
                return NotFound(result.FirstError.Description);

            return ValidationErrors(result.Errors);
        }

        return Ok(AnimalResponse.FromDomain(result.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Store(
        [FromForm]
        CreateAnimalRequest request)
    {
        AnimalSpecies species;
        AnimalGender gender;

        if (!Enum.TryParse(request.Species, true, out species))
            return Problem("Invalid species", statusCode: 400);

        if (!Enum.TryParse(request.Gender, true, out gender))
            return Problem("Invalid gender", statusCode: 400);

        // NOTE: For this to work there needs to exist an Images directory at the root of the web api project.
        // TODO: Automate the creation of this directory.

        string? imageUrl = null;

        if (request.Image is not null)
        {
            // TODO: We may want to ensure these filenames are unique.
            var filePath = Path.Combine(_environment.ContentRootPath, "Images", request.Image.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await request.Image.CopyToAsync(stream);
            _logger.LogInformation($"Stored image to path: {filePath}");
            imageUrl = $"{Request.Scheme}://{Request.Host}/images/{request.Image.FileName}";
        }

        var result = await _createAnimal.Invoke(new CreateAnimalParams
        {
            Name = request.Name,
            Age = request.Age,
            Gender = gender,
            Species = species,
            ImageUrl = imageUrl,
            Location = request.Location,
            Code = request.Code,
            Story = request.Story,
            Weight = request.Weight
        });

        if (result.IsError) return ValidationErrors(result.Errors);

        return Created(
            "/animals/" + result.Value.Id,
            AnimalResponse.FromDomain(result.Value));
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteAnimal.Invoke(id);

        if (result.IsError)
        {
            if (result.Errors.Count == 1 && result.FirstError.Type == ErrorType.NotFound)
                return NotFound(result.FirstError.Description);

            return ValidationErrors(result.Errors);
        }

        return Ok(result.Value);
    }
}
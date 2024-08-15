using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.Core.UseCases.Animals;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Throw;

namespace AmigosConCola.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/animals/")]
public class AnimalController : BaseApiController
{
    private readonly CountAllAnimalsUseCase _countAllAnimals;
    private readonly CreateAnimalUseCase _createAnimal;
    private readonly DeleteAnimalUseCase _deleteAnimal;
    private readonly GetAllAnimalsUseCase _getAllAnimals;
    private readonly GetAnimalByIdUseCase _getAnimalById;
    private readonly ILogger<AnimalController> _logger;
    private readonly IMapper _mapper;
    private readonly StoreAnimalImageUseCase _storeAnimalImage;
    private readonly UpdateAnimalUseCase _updateAnimal;
    private readonly UpdateAnimalImageUrlUseCase _updateAnimalImageUrlUseCase;

    public AnimalController(
        ILogger<AnimalController> logger,
        CreateAnimalUseCase createAnimal,
        GetAllAnimalsUseCase getAllAnimals,
        CountAllAnimalsUseCase countAllAnimals,
        GetAnimalByIdUseCase getAnimalById,
        DeleteAnimalUseCase deleteAnimal,
        UpdateAnimalUseCase updateAnimal,
        IMapper mapper,
        UpdateAnimalImageUrlUseCase updateAnimalImageUrlUseCase,
        StoreAnimalImageUseCase storeAnimalImage)
    {
        _logger = logger;
        _createAnimal = createAnimal;
        _getAllAnimals = getAllAnimals;
        _getAnimalById = getAnimalById;
        _countAllAnimals = countAllAnimals;
        _deleteAnimal = deleteAnimal;
        _updateAnimal = updateAnimal;
        _mapper = mapper;
        _updateAnimalImageUrlUseCase = updateAnimalImageUrlUseCase;
        _storeAnimalImage = storeAnimalImage;
    }

    [AllowAnonymous]
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

    [AllowAnonymous]
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

        // TODO: Move this validation to use case.
        if (!Enum.TryParse(request.Species, true, out species))
            return Problem("Invalid species", statusCode: 400);

        if (!Enum.TryParse(request.Gender, true, out gender))
            return Problem("Invalid gender", statusCode: 400);

        string? imageUrl = null;

        if (request.Image is not null)
        {
            await using var imageStream = request.Image.OpenReadStream();
            var uri = await _storeAnimalImage.Invoke(imageStream, request.Image.FileName);
            uri.IsError.Throw("Failed to store image").IfTrue();
            imageUrl = uri.Value.ToString();
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteAnimal.Invoke(id);

        if (result is { IsError: true, FirstError.Type: ErrorType.NotFound })
            return NotFound(result.FirstError.Description);

        result.IsError
            .Throw("Failed to delete animal")
            .IfTrue();

        return Ok(result.Value);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateAnimalRequest request)
    {
        var updateAnimalParams = _mapper.Map<UpdateAnimalParams>(request);
        var result = await _updateAnimal.Invoke(id, updateAnimalParams);

        if (result is { IsError: true, FirstError.Type: ErrorType.NotFound })
            return Problem(statusCode: 404, detail: result.FirstError.Description);

        result.IsError.Throw($"Failed to update the animal: {id}").IfTrue();

        var response = _mapper.Map<AnimalResponse>(result.Value);

        return Ok(response);
    }

    [HttpPut("{id:int}/image")]
    public async Task<IActionResult> UpdateImage(int id, UpdateAnimalImageUrlRequest request)
    {
        await using var imageStream = request.Image.OpenReadStream();
        var uri = await _storeAnimalImage.Invoke(imageStream, request.Image.FileName);
        uri.IsError.Throw("Failed to store image").IfTrue();

        var result = await _updateAnimalImageUrlUseCase.Invoke(id, uri.Value.ToString());
        if (result is { IsError: true, FirstError.Type: ErrorType.NotFound })
            return Problem(statusCode: 404, detail: result.FirstError.Description);

        result.IsError.Throw($"Failed to update the image for animal: {id}").IfTrue();

        return Ok(_mapper.Map<AnimalResponse>(result.Value));
    }
}
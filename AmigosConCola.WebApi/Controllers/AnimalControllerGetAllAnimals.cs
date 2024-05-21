using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animals/")]
public class AnimalControllerGetAllAnimals : ControllerBase
{
    private readonly CreateAnimalUseCase _createAnimal;
    private readonly GetAllAnimalsUseCase _getAllAnimals;

    public AnimalControllerGetAllAnimals(
        CreateAnimalUseCase createAnimal,
        GetAllAnimalsUseCase getAllAnimals)
    {
        _createAnimal = createAnimal;
        _getAllAnimals = getAllAnimals;
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
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return ValidationProblem(ModelState);
        }

        return Ok(result.Value.Select(AnimalResponse.FromDomain));
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
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return ValidationProblem(ModelState);
        }

        return Created(
            "/animals/" + result.Value.Id,
            AnimalResponse.FromDomain(result.Value));
    }
}
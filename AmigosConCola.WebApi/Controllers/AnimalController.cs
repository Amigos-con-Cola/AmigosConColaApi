using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

[ApiController]
[Route("/api/animals/")]
public class AnimalController : ControllerBase
{
    private readonly CreateAnimalUseCase _createAnimal;

    public AnimalController(
        CreateAnimalUseCase createAnimal)
    {
        _createAnimal = createAnimal;
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
            ImageUrl = request.ImageUrl
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
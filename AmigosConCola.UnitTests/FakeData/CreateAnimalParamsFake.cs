using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.UnitTests.Extensions;

public static class CreateAnimalParamsFake
{
    public static CreateAnimalParams Get(
        int? age = null,
        string? name = null,
        string? imageUrl = null)
    {
        var random = new Randomizer();
        var names = new Name();
        var url = new Images();
        return new CreateAnimalParams
        {
            Age = age ?? random.Int(1, 20),
            Name = name ?? names.FirstName(),
            Gender = AnimalGender.Male,
            ImageUrl = imageUrl ?? url.PicsumUrl(),
            Species = AnimalSpecies.Cat
        };
    }
}
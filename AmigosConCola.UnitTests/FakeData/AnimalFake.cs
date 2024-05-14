using AmigosConCola.Core.Models;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.UnitTests.Extensions;

public class AnimalFake
{
    public static Animal Get(
        int? age = null,
        string? name = null,
        string? imageUrl = null)
    {
        var random = new Randomizer();
        var names = new Name();
        var url = new Images();
        return new Animal
        {
            Id = random.Int(),
            Age = age ?? random.Int(1, 20),
            Name = name ?? names.FirstName(),
            Gender = AnimalGender.Male,
            ImageUrl = imageUrl ?? url.PicsumUrl(),
            Species = AnimalSpecies.Cat
        };
    }
}
using AmigosConCola.Core.Models;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.UnitTests.FakeData;

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
        var lorem = new Lorem();
        return new Animal
        {
            Id = random.Int(),
            Age = age ?? random.Int(1, 20),
            Name = name ?? names.FirstName(),
            Gender = AnimalGender.Male,
            ImageUrl = imageUrl ?? url.PicsumUrl(),
            Species = AnimalSpecies.Cat,
            Story = lorem.Paragraph(),
            Weight = random.Number(),
            Location = random.String(),
            Code = random.String()
        };
    }
}
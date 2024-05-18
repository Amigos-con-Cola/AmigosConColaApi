using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.UnitTests.FakeData;

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
        var lorem = new Lorem();
        return new CreateAnimalParams
        {
            Age = age ?? random.Int(1, 20),
            Name = name ?? names.FirstName(),
            Gender = AnimalGender.Male,
            ImageUrl = imageUrl ?? url.PicsumUrl(),
            Species = AnimalSpecies.Cat,
            Story = lorem.Paragraph(),
            Code = random.String(),
            Weight = random.Number(),
            Location = random.String()
        };
    }
}
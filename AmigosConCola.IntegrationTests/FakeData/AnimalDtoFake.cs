using AmigosConCola.WebApi.Data.Entities;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.IntegrationTests.FakeData;

public class AnimalDtoFake
{
    public static AnimalEntity Get(
        int? age = null,
        string? name = null,
        string? imageUrl = null,
        string? species = null)
    {
        var random = new Randomizer();
        var names = new Name();
        var url = new Images();
        var lorem = new Lorem();
        return new AnimalEntity
        {
            Id = random.Int(),
            Age = age ?? random.Int(1, 20),
            Name = name ?? names.FirstName(),
            Gender = random.ArrayElement(["Male", "Female"]),
            ImageUrl = imageUrl ?? url.PicsumUrl(),
            Species = species ?? random.ArrayElement(["Cat", "Dog"]),
            Story = lorem.Paragraph(),
            Weight = random.Number(),
            Location = lorem.Word(),
            Code = lorem.Word()
        };
    }
}
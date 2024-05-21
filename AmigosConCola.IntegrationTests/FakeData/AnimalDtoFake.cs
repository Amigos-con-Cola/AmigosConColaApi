using AmigosConCola.WebApi.Data.Dto;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.IntegrationTests.FakeData;

public class AnimalDtoFake
{
    public static AnimalDto Get(
        int? age = null,
        string? name = null,
        string? imageUrl = null)
    {
        var random = new Randomizer();
        var names = new Name();
        var url = new Images();
        var lorem = new Lorem();
        return new AnimalDto
        {
            Id = random.Int(),
            Age = age ?? random.Int(1, 20),
            Name = name ?? names.FirstName(),
            Gender = random.ArrayElement(new[] { "Male", "Female" }),
            ImageUrl = imageUrl ?? url.PicsumUrl(),
            Species = random.ArrayElement(new[] { "Cat", "Dog" }),
            Story = lorem.Paragraph(),
            Weight = random.Number(),
            Location = lorem.Word(),
            Code = lorem.Word()
        };
    }
}
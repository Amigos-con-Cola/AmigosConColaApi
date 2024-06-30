using AmigosConCola.WebApi.Presentation;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.IntegrationTests.FakeData;

public class CreateAnimalRequestFake
{
    public static CreateAnimalRequest Get()
    {
        var name = new Name();
        var random = new Randomizer();
        var lorem = new Lorem();

        return new CreateAnimalRequest
        {
            Name = name.FirstName(),
            Age = random.Number(1, 20),
            Gender = random.ArrayElement(new[] { "Male", "Female" }),
            Image = null,
            Species = random.ArrayElement(new[] { "Dog", "Cat" }),
            Story = lorem.Paragraph(),
            Location = lorem.Word(),
            Weight = random.Number(),
            Code = lorem.Word()
        };
    }
}

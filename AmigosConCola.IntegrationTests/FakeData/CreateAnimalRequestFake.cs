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
            ImageUrl = random.String(),
            Species = random.ArrayElement(new[] { "Dog", "Cat" }),
            Story = lorem.Paragraph(),
            Location = random.String(),
            Weight = random.Number(),
            Code = random.String()
        };
    }
}
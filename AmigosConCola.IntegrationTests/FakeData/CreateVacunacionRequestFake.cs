using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Requests;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.IntegrationTests.FakeData;

public class CreateVacunacionRequestFake
{
    public static CreateVacunacionRequest Get()
    {
        var name = new Name();
        var random = new Randomizer();
        var date = new Date();

        return new CreateVacunacionRequest
        {
            Name = name.FirstName(),
            Date = date.FutureDateOnly(),
            ExamenPrevio = random.String2(10)
        };
    }
}
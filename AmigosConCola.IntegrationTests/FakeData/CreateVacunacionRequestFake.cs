using AmigosConCola.WebApi.Presentation;
using Bogus.DataSets;

namespace AmigosConCola.IntegrationTests.FakeData;

public class CreateVacunacionRequestFake
{
    public static CreateVacunacionRequest Get()
    {
        var name = new Name();
        var date = new Date();

        return new CreateVacunacionRequest
        {
            Name = name.FirstName(),
            Date = date.FutureDateOnly()
        };
    }
}